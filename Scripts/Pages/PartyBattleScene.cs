using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Barter.Classes;
using Barter.Managers;
using SimpleJSON;
using BayatGames.SaveGameFree;
public class PartyBattleScene : MonoBehaviour
{
    public Text monsterName;
    public Text monsterTime;
    public GameObject image;

    public GameObject CharacterSmallPrefab;

    Sprite sprite;
    // Start is called before the first frame update
    public GameObject partyObject;
    void Start()
    {
        int bossID = StaticClass.CrossSceneMonster.id;
        sprite = Resources.Load<Sprite>("boss" + bossID.ToString());
        image.GetComponent<Image>().sprite = sprite;

        monsterName.text = StaticClass.CrossSceneMonster.name;
        int tempDuration = StaticClass.CrossSceneMonster.duration / 60;
        monsterTime.text = tempDuration.ToString() + " mins";
        RenderParty(partyObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RenderParty(GameObject partyObject)
    {
        AddMemberToGrid(partyObject, StaticClass.SelectedCharacter);
        if (StaticClass.CharacterOne != null)
        {
            AddMemberToGrid(partyObject, StaticClass.CharacterOne);
            if (StaticClass.CharacterTwo != null)
            {
                AddMemberToGrid(partyObject, StaticClass.CharacterTwo);
                if (StaticClass.CharacterThree != null)
                {
                    AddMemberToGrid(partyObject, StaticClass.CharacterThree);
                }
            }
        }
    }
    public void toRewardsPage() {
        SceneManager.LoadScene("RewardsPage");
    }
    void AddMemberToGrid(GameObject partyObject, Character normalCharacter)
    {
        GameObject characterPrefab = Instantiate(CharacterSmallPrefab) as GameObject;
        CharacterSmallController controller = characterPrefab.GetComponent<CharacterSmallController>();
        controller.character = normalCharacter;
        controller.Name.text = normalCharacter.name;
        controller.Ready.gameObject.SetActive(false);
        controller.transform.parent = partyObject.transform;
    }
}
