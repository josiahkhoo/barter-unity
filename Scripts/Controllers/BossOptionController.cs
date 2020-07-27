using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Barter.Classes;
using Barter.Managers;

public class BossOptionController : MonoBehaviour
// Responsible for the rendering of individual options
{
    // Start is called before the first frame update
    public Monster monster;
    public Text Name;
    public Text Level;
    public Text Duration;
    public GameObject image;
    //FIXME: Can add more random shit here in the future like avatar and level

    public void OnClickFightButton()
    {
        StaticClass.CrossSceneMonster = monster;
        StartCoroutine(BattleManager.createBattle(monster, StaticClass.SelectedCharacter, () =>
        {

            SceneManager.LoadScene("BattleScene");
        }));
    }

    public void PatyQuestOnClick()
    {
        StaticClass.CrossSceneMonster = monster;
        SceneManager.LoadScene("PartyBattleScene");
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    Sprite sprite;
    void Start()
    {
        int bossID = monster.id;
        sprite = Resources.Load <Sprite>("sets/set" + bossID.ToString());
		image.GetComponent<Image>().sprite = sprite;

    }

}
