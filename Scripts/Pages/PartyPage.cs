using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Barter.Classes;
using Barter.Managers;
using SimpleJSON;

public class PartyPage : MonoBehaviour
{
    public Text RoomNumber;
    public GameObject confirmLeaveBox;

    public GameObject unreadyButton;
    public GameObject message;

    public GameObject GridCharactersSmall;
    public GameObject CharacterPrefab;
    public GameObject BossOptionPrefab;
    private string message_text;
    public int state;

    public int chatId;

    public List<Character> allCharacters;
    public List<Character> readyCharacters;

    public bool allReady;



    public void backButton()
    {
        this.confirmLeaveBox.SetActive(true);
    }

    public void cancelConfirmLeaveBoxButton()
    {
        this.confirmLeaveBox.SetActive(false);
    }

    public void confirmLeaveButton()
    {
        SceneManager.LoadScene("QuestPage");
    }

    public void readyButoon()
    {
        this.unreadyButton.SetActive(true);
        this.state = 1;
        message_text = "I'm ready!";
        sendMessage();
    }

    public void unreadyButtonOnClick()
    {
        this.state = 0;
        this.unreadyButton.SetActive(false);
        message_text = "I'm not ready...";
        sendMessage();
    }


    // Start is called before the first frame update
    void Start()
    {
        // Reset storage
        StaticClass.CharacterOne = null;
        StaticClass.CharacterTwo = null;
        StaticClass.CharacterThree = null;
        //0 means not ready
        this.state = 0;
        this.confirmLeaveBox.SetActive(false);
        this.unreadyButton.SetActive(false);
        this.chatId = StaticClass.CrossSceneChatInt;
        RoomNumber.text = "Room Number : " + StaticClass.CrossSceneParty.accessCode;
        Monster monster = StaticClass.CrossSceneMonster;
        print(monster);
        PartyBossOptionController controller = BossOptionPrefab.GetComponent<PartyBossOptionController>();
        controller.monster = monster;
        controller.Name.text = monster.name;
        // controller.Level.text = string.Format("Lvl: {0}", monster.level.ToString());
        controller.PartySize.text = string.Format("Size: {0}", monster.partySize.ToString());
        controller.Duration.text = string.Format("{0} minutes", (monster.duration / 60).ToString());
        // sets parent of the character option to the grid
        StartCoroutine(pollParty(StaticClass.CrossSceneParty, StaticClass.SelectedCharacter));
    }

    // Update is called once per frame
    void Update()
    {
        message_text = message.GetComponent<InputField>().text;
    }

    void StartBattle()
    {
        StartCoroutine(BattleManager.createBattle(StaticClass.CrossSceneMonster, StaticClass.SelectedCharacter, () =>
        {
            SceneManager.LoadScene("PartyBattleScene");
        }));
    }

    public IEnumerator pollParty(Party party, Character selectedCharacter)
    {
        string url = string.Format("https://backend.josiahkhoo.me/api/parties/{0}/poll/", party.accessCode);
        string token = string.Format("Token {0}", PlayerPrefs.GetString("token", "").ToString());
        WWWForm form = new WWWForm();
        form.AddField("character", selectedCharacter.id);
        form.AddField("state", this.state);
        using (var request = UnityWebRequest.Post(url, form))
        {
            request.SetRequestHeader("Authorization", token);
            yield return request.SendWebRequest();
            // Debug.Log(request.downloadHandler.text);
            var response = JSON.Parse(request.downloadHandler.text);
            // Debug.Log(response);
            if (request.responseCode == 200)
            {
                // Debug.Log(response.ToString());
                allCharacters = new List<Character>();
                readyCharacters = new List<Character>();
                var readyCharactersJson = response["data"]["ready_characters"];
                var allCharactersJson = response["data"]["all_characters"];
                for (int i = 0; i < readyCharactersJson.Count; i++)
                {
                    var readyCharacterJson = readyCharactersJson[i];
                    Character readyCharacter = CharacterManager.GetCharacterFromString(readyCharacterJson.ToString());
                    readyCharacters.Add(readyCharacter);
                }
                for (int i = 0; i < allCharactersJson.Count; i++)
                {
                    var nonReadyCharacterJson = allCharactersJson[i];
                    Character nonReadyCharacter = CharacterManager.GetCharacterFromString(nonReadyCharacterJson.ToString());
                    allCharacters.Add(nonReadyCharacter);
                }
                allReady = response["data"]["all_ready"];
                renderCharacters();
                if (allReady)
                {
                    readyCharacters.Remove(StaticClass.SelectedCharacter);
                    for (int i = 0; i < readyCharacters.Count; i++)
                    {
                        if (i == 0)
                        {
                            StaticClass.CharacterOne = readyCharacters[i];
                        }
                        else if (i == 1)
                        {
                            StaticClass.CharacterTwo = readyCharacters[i];
                        }
                        else if (i == 2)
                        {
                            StaticClass.CharacterThree = readyCharacters[i];
                        }
                    }
                    StartBattle();
                }
            }
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(pollParty(party, selectedCharacter));
    }

    private void renderCharacters()
    {
        foreach (Transform child in GridCharactersSmall.transform)
        {
            // DESTROY ALL CHILDREN
            GameObject.Destroy(child.gameObject);
        }
        foreach (Character normalCharacter in allCharacters)
        {
            GameObject characterPrefab = Instantiate(CharacterPrefab) as GameObject;
            CharacterSmallController controller = characterPrefab.GetComponent<CharacterSmallController>();
            controller.character = normalCharacter;
            controller.Name.text = normalCharacter.name;
            if (readyCharacters.Contains(normalCharacter))
            {
                controller.Ready.gameObject.SetActive(true);
            }
            else
            {
                controller.Ready.gameObject.SetActive(false);
            }
            controller.transform.parent = GridCharactersSmall.transform;
        }
    }

    public void sendMessage()
    {
        //get username here
        //send
        print("<" + message_text + ">" + " sent");
        StartCoroutine(UserManager.SendMessage(chatId, message_text, () =>
        {
            message.GetComponent<InputField>().text = "";
        }));
    }
}
