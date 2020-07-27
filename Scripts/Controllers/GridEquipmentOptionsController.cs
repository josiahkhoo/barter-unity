using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using Barter.Classes;
using SimpleJSON;
using Barter.Managers;
using System;

public class GridEquipmentOptionsController : MonoBehaviour
// Responsible for the control of the entire grid.
{
    public GameObject GridEquipmentOptions;
    public GameObject EquipmentOptionPrefab;

    public Text Header;

    public Text Shadow;
    void Start()
    {
        int type = StaticClass.CrossSceneEquipmentType;
        if (type == 0)
        {
            Header.text = "Helmet";
            Shadow.text = "Helmet";
        }
        else if (type == 1)
        {
            Header.text = "Armour";
            Shadow.text = "Armour";
        }
        else if (type == 2)
        {
            Header.text = "Off Hand";
            Shadow.text = "Off Hand";
        }
        else if (type == 3)
        {
            Header.text = "Weapon";
            Shadow.text = "Weapon";
        }
        StartCoroutine(GetEquipments(StaticClass.SelectedCharacter, StaticClass.CrossSceneEquipmentType, null));
    }

    public IEnumerator GetEquipments(Character character, int type, Action callback)
    {
        foreach (Transform child in GridEquipmentOptions.transform)
        {
            // DESTROY ALL CHILDREN
            GameObject.Destroy(child.gameObject);
        }
        List<Equipment> equipments = new List<Equipment>();
        string url = String.Format("https://backend.josiahkhoo.me/api/equipments?character_id={0}&equipment_type={1}", character.id.ToString(), type.ToString());
        string token = string.Format("Token {0}", PlayerPrefs.GetString("token", "").ToString());
        using (var request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Authorization", token);
            yield return request.SendWebRequest();
            var response = JSON.Parse(request.downloadHandler.text);
            if (request.responseCode == 200)
            {
                print(response);
                var equipmentsArray = response["equipments"];
                for (int i = 0; i < equipmentsArray.Count; i++)
                {
                    var equipmentJson = equipmentsArray[i];
                    Equipment equipment = EquipmentManager.GetEquipmentFromString(equipmentJson.ToString());
                    equipments.Add(equipment);
                }
            }
        }
        foreach (Equipment equipment in equipments)
        {
            print(equipment.name);
            GameObject equipmentOption = Instantiate(EquipmentOptionPrefab) as GameObject;
            EquipmentOptionController controller = equipmentOption.GetComponent<EquipmentOptionController>();
            controller.Name.text = equipment.name;
            controller.equipment = equipment;
            controller.character = character;
            controller.transform.parent = GridEquipmentOptions.transform;
        }
        if (callback != null) { callback(); }
    }
}
