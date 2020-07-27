using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using Barter.Managers;
using Barter.Classes;

public class RewardsPage : MonoBehaviour
{
    public GameObject rewardsFlare;

    public GameObject reward;

    public GameObject exitButton;

    public GameObject equipmentObject;

    public Equipment equipment;

    public void openButtonOnClick()
    {
        //get random reward from backend
        //change reward picture and add it to inventory
        this.exitButton.SetActive(true);
        this.rewardsFlare.SetActive(true);
        EquipmentManager.RenderEquipment(equipmentObject, equipment);
    }
    public void toQuestPage()
    {
        SceneManager.LoadScene("QuestPage");
    }
    public void exitButtonOnClick()
    {
        toQuestPage();
    }


    // Start is called before the first frame update
    void Start()
    {
        // Extract reward from battle
        Battle battle = StaticClass.CrossSceneBattle;
        this.equipment = battle.equipment;
        // EquipmentController controller = equipmentObject.GetComponent<EquipmentController>();
        // GameObject equip = controller.Armour;
        // Sprite[] allSprites = Resources.LoadAll<Sprite>("Basic/HunterLightArmor");
        // foreach (Sprite sprite in allSprites)
        // {
        //     String part = string.Format("{0}[Armor]", sprite.ToString().Split(' ')[0]);
        //     GameObject subObject = EquipmentManager.FindChildByRecursion(equip.transform, part).gameObject;
        //     SpriteRenderer subSprite = subObject.GetComponent<SpriteRenderer>();
        //     subSprite.sprite = sprite;
        // }
        // equip.SetActive(true);
        this.rewardsFlare.SetActive(false);
        this.reward.SetActive(false);
        this.exitButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
