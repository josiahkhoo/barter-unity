using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CharacterInternal = Barter.Classes.Character;
using Storage = Barter.Classes.Storage;
using Assets.HeroEditor.Common.CharacterScripts;
using StaticClass = Barter.Classes.StaticClass;
using Barter.Managers;

public class InventoryPage : MonoBehaviour
{
    public CharacterInternal characterInternal;
    public Text Name;
    public GameObject CharacterObject;

    public GameObject Helmet;
    public GameObject Armour;
    public GameObject Shield;
    public GameObject Weapon;

    public GameObject GridEquipmentOptions;
    public void toFriendsTab()
    {
        SceneManager.LoadScene("SocialPageFriends");
    }

    public void toQuestPage()
    {
        SceneManager.LoadScene("QuestPage");
    }

    int part = -1;
    public void changeBody()
    {
        StaticClass.CrossSceneEquipmentType = 1;
        SceneManager.LoadScene("SelectEquipmentPage");
    }
    public void changeHead()
    {
        StaticClass.CrossSceneEquipmentType = 0;
        SceneManager.LoadScene("SelectEquipmentPage");
    }
    public void changeWeapon()
    {
        StaticClass.CrossSceneEquipmentType = 3;
        // GridEquipmentOptionsController controller = GridEquipmentOptions.GetComponent<GridEquipmentOptionsController>();
        // StartCoroutine(controller.GetEquipments(characterInternal, part, () =>
        // {
        //     this.toEquipList.SetActive(true);
        // }));
        SceneManager.LoadScene("SelectEquipmentPage");
    }
    public void changeShield()
    {
        StaticClass.CrossSceneEquipmentType = 2;
        SceneManager.LoadScene("SelectEquipmentPage");
    }

    // Start is called before the first frame update
    void Start()
    {
        characterInternal = StaticClass.SelectedCharacter;
        CharacterManager.InstantiateCharacterObject(CharacterObject, characterInternal);
        EquipmentManager.RenderHelmetFromCharacterConfig(Helmet, characterInternal);
        EquipmentManager.RenderArmourFromCharacterConfig(Armour, characterInternal);
        EquipmentManager.RenderShieldFromCharacterConfig(Shield, characterInternal);
        EquipmentManager.RenderWeaponFromCharacterConfig(Weapon, characterInternal);
        Name.text = characterInternal.name;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
