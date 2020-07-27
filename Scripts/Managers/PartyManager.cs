using SimpleJSON;
using System.Collections.Generic;
using System.Collections;
using Barter.Classes;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using System;

namespace Barter.Managers
{
    public static class PartyManager
    {
        public static Party GetPartyFromString(string partyString)
        {

            var partyJson = JSON.Parse(partyString);
            int id = partyJson["id"];
            int chatId = partyJson["chat"]["id"];
            string accessCode = partyJson["access_code"];
            Monster monster = MonsterManager.GetMonsterFromString(partyJson["monster"].ToString());
            return new Party(id, chatId, accessCode, monster);
        }

        public static IEnumerator createParty(Monster monster, Action callback)
        {
            string url = "https://backend.josiahkhoo.me/api/parties/";
            string token = string.Format("Token {0}", PlayerPrefs.GetString("token", "").ToString());
            WWWForm form = new WWWForm();
            // Debug.Log(url);
            form.AddField("monster", monster.id);
            using (var request = UnityWebRequest.Post(url, form))
            {
                request.SetRequestHeader("Authorization", token);
                yield return request.SendWebRequest();
                var response = JSON.Parse(request.downloadHandler.text);
                // Debug.Log(response);
                if (request.responseCode == 200)
                {
                    var partyJson = response["data"]["party"];
                    Party party = PartyManager.GetPartyFromString(partyJson.ToString());
                    // Debug.Log(party);
                    StaticClass.CrossSceneParty = party;
                    if (callback != null) { callback(); }
                }
            }
        }


        public static IEnumerator joinParty(String accessCode, Action callback, Action fallback)
        {
            string url = string.Format("https://backend.josiahkhoo.me/api/parties/join/{0}", accessCode);
            string token = string.Format("Token {0}", PlayerPrefs.GetString("token", "").ToString());
            WWWForm form = new WWWForm();
            // Debug.Log(url);
            using (var request = UnityWebRequest.Post(url, form))
            {
                request.SetRequestHeader("Authorization", token);
                yield return request.SendWebRequest();
                var response = JSON.Parse(request.downloadHandler.text);
                // Debug.Log(response);
                if (request.responseCode == 200)
                {
                    var partyJson = response["data"]["party"];
                    Party party = PartyManager.GetPartyFromString(partyJson.ToString());
                    // Debug.Log(party);
                    StaticClass.CrossSceneParty = party;
                    StaticClass.CrossSceneChatInt = party.chatId;
                    StaticClass.CrossSceneMonster = party.monster;
                    if (callback != null) { callback(); }
                }
                else
                {
                    if (fallback != null) { fallback(); }
                }
            }
        }

    }
}