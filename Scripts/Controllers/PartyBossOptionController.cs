using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Barter.Classes;
using Barter.Managers;
using SimpleJSON;

public class PartyBossOptionController : MonoBehaviour
// Responsible for the rendering of individual options
{
    // Start is called before the first frame update
    public Monster monster;
    public Text Name;
    public Text Level;
    public Text Duration;
    public Text PartySize;

    public GameObject image;
    //FIXME: Can add more random shit here in the future like avatar and level

    public void OnClickCreateButton()
    {
        Debug.Log("CALLED");
        StaticClass.CrossSceneMonster = monster;
        StartCoroutine(PartyManager.createParty(monster, () =>
        {
            StaticClass.CrossSceneChatInt = StaticClass.CrossSceneParty.chatId;
            SceneManager.LoadScene("PartyPage");
        }));
    }

    Sprite sprite;
    void Start()
    {
        if (monster == null)
        {
            monster = StaticClass.CrossSceneMonster;
        }
        int bossID = monster.id;

        sprite = Resources.Load<Sprite>("sets/set" + bossID.ToString());
        image.GetComponent<Image>().sprite = sprite;

    }

}
