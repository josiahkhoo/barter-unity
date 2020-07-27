using SimpleJSON;
using System.Collections.Generic;
using System.Collections;
using Barter.Classes;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

namespace Barter.Managers
{
    public static class EquipmentManager
    {
        public static Equipment GetEquipmentFromString(string equipmentString)
        {
            var equipmentJson = JSON.Parse(equipmentString);
            int id = equipmentJson["id"];
            string name = equipmentJson["name"];
            int type = equipmentJson["equipment_type"];
            string apperanceConfig = equipmentJson["appearance_config"].ToString();
            return new Equipment(id, name, type, apperanceConfig);
        }

        public static void RenderEquipment(GameObject equipmentObject, Equipment equipment)
        {
            EquipmentController controller = equipmentObject.GetComponent<EquipmentController>();
            var config = JSON.Parse(equipment.appearanceConfig);
            GameObject equip = null;
            String itemName = null;
            if (equipment.type == 0)
            {
                equip = controller.Helmet;
                itemName = config["ValueList"][5];
                SpriteRenderer spriteR = equip.GetComponent<SpriteRenderer>();
                spriteR.sprite = Resources.Load<Sprite>(itemName);
                equip.SetActive(true);
            }
            if (equipment.type == 1)
            {
                equip = controller.Armour;
                itemName = config["ValueList"][8];
                Sprite[] allSprites = Resources.LoadAll<Sprite>(itemName);
                foreach (Sprite sprite in allSprites)
                {
                    String part = string.Format("{0}[Armor]", sprite.ToString().Split(' ')[0]);
                    GameObject subObject = FindChildByRecursion(equip.transform, part).gameObject;
                    SpriteRenderer subSprite = subObject.GetComponent<SpriteRenderer>();
                    // Debug.Log(sprite);
                    // Debug.Log(subObject);
                    subSprite.sprite = sprite;
                }
                equip.SetActive(true);
            }
            if (equipment.type == 2)
            {
                equip = controller.Shield;
                itemName = config["ValueList"][13];
                SpriteRenderer spriteR = equip.GetComponent<SpriteRenderer>();
                spriteR.sprite = Resources.Load<Sprite>(itemName);
                equip.SetActive(true);
            }
            if (equipment.type == 3)
            {
                equip = controller.Weapon;
                itemName = config["ValueList"][9];
                SpriteRenderer spriteR = equip.GetComponent<SpriteRenderer>();
                spriteR.sprite = Resources.Load<Sprite>(itemName);
                equip.SetActive(true);
            }
        }

        public static void RenderHelmetFromCharacterConfig(GameObject equipmentObject, Character character)
        {
            EquipmentController controller = equipmentObject.GetComponent<EquipmentController>();
            var config = JSON.Parse(character.appearanceConfig);
            GameObject equip = controller.Helmet;
            if (equip == null)
            {
                return;
            }
            String itemName = config["ValueList"][5];
            if (itemName != "")
            {
                SpriteRenderer spriteR = equip.GetComponent<SpriteRenderer>();
                spriteR.sprite = Resources.Load<Sprite>(itemName);
                equip.SetActive(true);
            }
        }

        public static void RenderShieldFromCharacterConfig(GameObject equipmentObject, Character character)
        {
            EquipmentController controller = equipmentObject.GetComponent<EquipmentController>();
            var config = JSON.Parse(character.appearanceConfig);
            GameObject equip = controller.Shield;
            if (equip == null)
            {
                return;
            }
            String itemName = config["ValueList"][13];
            // Debug.Log(itemName);
            if (itemName != "")
            {
                SpriteRenderer spriteR = equip.GetComponent<SpriteRenderer>();
                spriteR.sprite = Resources.Load<Sprite>(itemName);
                equip.SetActive(true);
            }
        }

        public static void RenderWeaponFromCharacterConfig(GameObject equipmentObject, Character character)
        {
            EquipmentController controller = equipmentObject.GetComponent<EquipmentController>();
            var config = JSON.Parse(character.appearanceConfig);
            GameObject equip = controller.Weapon;
            if (equip == null)
            {
                return;
            }
            String itemName = config["ValueList"][9];
            // Debug.Log(itemName);
            if (itemName != "")
            {
                SpriteRenderer spriteR = equip.GetComponent<SpriteRenderer>();
                spriteR.sprite = Resources.Load<Sprite>(itemName);
                equip.SetActive(true);
            }
        }


        public static void RenderArmourFromCharacterConfig(GameObject equipmentObject, Character character)
        {
            EquipmentController controller = equipmentObject.GetComponent<EquipmentController>();
            var config = JSON.Parse(character.appearanceConfig);
            GameObject equip = controller.Armour;
            if (equip == null)
            {
                return;
            }
            String itemName = config["ValueList"][8];
            // Debug.Log(itemName);
            Sprite[] allSprites = Resources.LoadAll<Sprite>(itemName);
            foreach (Sprite sprite in allSprites)
            {
                String part = string.Format("{0}[Armor]", sprite.ToString().Split(' ')[0]);
                GameObject subObject = FindChildByRecursion(equip.transform, part).gameObject;
                SpriteRenderer subSprite = subObject.GetComponent<SpriteRenderer>();
                // Debug.Log(sprite);
                // Debug.Log(subObject);
                subSprite.sprite = sprite;
            }
            equip.SetActive(true);
        }

        internal static Transform FindChildByRecursion(this Transform aParent, string aName)
        {
            if (aParent == null) return null;
            var result = aParent.Find(aName);
            if (result != null)
                return result;
            foreach (Transform child in aParent)
            {
                result = child.FindChildByRecursion(aName);
                if (result != null)
                    return result;
            }
            return null;
        }
    }


}