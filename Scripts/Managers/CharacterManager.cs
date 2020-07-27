using SimpleJSON;
using Storage = Barter.Classes.Storage;
using CharacterInternal = Barter.Classes.Character;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine.Networking;
using UnityEngine.Events;
using System;
using System.Collections;
using StaticClass = Barter.Classes.StaticClass;
using Barter.Classes;

namespace Barter.Managers
{
    public static class CharacterManager
    {
        public static CharacterInternal GetCharacterFromString(string characterString)
        {
            var characterJson = JSON.Parse(characterString);
            int characterId = characterJson["id"];
            string name = characterJson["name"];
            var appearanceConfig = characterJson["appearance_config"];
            bool isActive = characterJson["is_active"];
            CharacterInternal character = new CharacterInternal(characterId, name, appearanceConfig.ToString(), isActive);
            return character;
        }
        public static void InstantiateCharacterObject(GameObject CharacterObject, CharacterInternal characterInternal)
        {
            if (Storage.User == null || StaticClass.SelectedCharacter == null)
            {
                SceneManager.LoadScene("Login Page");
            }
            else
            {
                if (characterInternal == null)
                {
                    characterInternal = StaticClass.SelectedCharacter;
                }
                Assets.HeroEditor.Common.CharacterScripts.Character character = CharacterObject.GetComponent<Assets.HeroEditor.Common.CharacterScripts.Character>();
                character.LoadFromJson(characterInternal.appearanceConfig.ToString());
            }
        }

        public static IEnumerator EquipItem(CharacterInternal character, Equipment equipment, Action callback)
        {
            string url = string.Format("https://backend.josiahkhoo.me/api/equipments/{0}", equipment.id.ToString());
            string token = string.Format("Token {0}", PlayerPrefs.GetString("token", "").ToString());
            WWWForm form = new WWWForm();
            form.AddField("character_id", character.id);
            using (var request = UnityWebRequest.Post(url, form))
            {
                request.SetRequestHeader("Authorization", token);
                yield return request.SendWebRequest();
                var response = JSON.Parse(request.downloadHandler.text);
                if (request.responseCode == 200)
                {
                    var characterJson = response["data"]["character"];
                    CharacterInternal characterInternal = GetCharacterFromString(characterJson.ToString());
                    StaticClass.SelectedCharacter = characterInternal;
                }
                if (callback != null) { callback(); }
            }
        }

    }
}
