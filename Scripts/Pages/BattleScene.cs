using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Barter.Classes;
using Barter.Managers;
using SimpleJSON;
using BayatGames.SaveGameFree;

public class BattleScene : MonoBehaviour
{
    public Text monsterName;
    public Text monsterTime;
    public GameObject image;

    public Text charaName;

    public void toQuestPage()
    {
        SceneManager.LoadScene("QuestPage");
    }

    public void toRewardsPage() {
        SceneManager.LoadScene("RewardsPage");
    }

	Sprite sprite;
	// Use this for initialization
	
    // Start is called before the first frame update
    void Start()
    {   int bossID = StaticClass.CrossSceneMonster.id;
        sprite = Resources.Load <Sprite>("boss" + bossID.ToString());
		image.GetComponent<Image>().sprite = sprite;

        monsterName.text = StaticClass.CrossSceneMonster.name;
        int tempDuration = StaticClass.CrossSceneMonster.duration / 60;
        monsterTime.text = tempDuration.ToString() + " mins";
        charaName.text = StaticClass.SelectedCharacter.name;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
