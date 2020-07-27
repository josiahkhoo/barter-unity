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
    public static class BattleManager
    {
        public static Battle GetBattleFromString(string battleString)
        {

            var battleJson = JSON.Parse(battleString);
            int id = battleJson["id"];
            DateTime datetimeCreated = DateTime.Parse(battleJson["datetime_created"]);
            Monster monster = MonsterManager.GetMonsterFromString(battleJson["monster"].ToString());
            int state = battleJson["state"];
            Equipment equipment = null;
            if (battleJson["equipment"] != null)
            {
                equipment = EquipmentManager.GetEquipmentFromString(battleJson["equipment"].ToString());
            }
            return new Battle(id, monster, datetimeCreated, state, equipment);
        }

        public static IEnumerator createBattle(Monster monster, Character character, Action callback)
        {
            string url = "https://backend.josiahkhoo.me/api/battles/";
            string token = string.Format("Token {0}", PlayerPrefs.GetString("token", "").ToString());
            WWWForm form = new WWWForm();
            form.AddField("monster", monster.id);
            form.AddField("character", character.id);
            using (var request = UnityWebRequest.Post(url, form))
            {
                request.SetRequestHeader("Authorization", token);
                yield return request.SendWebRequest();
                var response = JSON.Parse(request.downloadHandler.text);
                if (request.responseCode == 200)
                {
                    var battleJson = response["data"]["battle"];
                    Battle battle = BattleManager.GetBattleFromString(battleJson.ToString());
                    StaticClass.CrossSceneBattle = battle;
                    if (callback != null) { callback(); }
                }
            }
        }

        public static IEnumerator completeBattle(Battle battle, Action callback)
        {
            string url = string.Format("https://backend.josiahkhoo.me/api/battles/{0}/complete/", battle.id);
            string token = string.Format("Token {0}", PlayerPrefs.GetString("token", "").ToString());
            WWWForm form = new WWWForm();
            using (var request = UnityWebRequest.Post(url, form))
            {
                request.SetRequestHeader("Authorization", token);
                yield return request.SendWebRequest();
                var response = JSON.Parse(request.downloadHandler.text);
                if (request.responseCode == 200)
                {
                    var battleJson = response["data"]["battle"];
                    Battle updatedBattle = BattleManager.GetBattleFromString(battleJson.ToString());
                    StaticClass.CrossSceneBattle = updatedBattle;
                    if (callback != null) { callback(); }
                }
            }
        }


        // public static IEnumerator joinParty(String accessCode, Action callback)
        // {
        //     string url = string.Format("https://backend.josiahkhoo.me/api/parties/join/{0}", accessCode);
        //     string token = string.Format("Token {0}", PlayerPrefs.GetString("token", "").ToString());
        //     WWWForm form = new WWWForm();
        //     Debug.Log(url);
        //     using (var request = UnityWebRequest.Post(url, form))
        //     {
        //         request.SetRequestHeader("Authorization", token);
        //         yield return request.SendWebRequest();
        //         var response = JSON.Parse(request.downloadHandler.text);
        //         Debug.Log(response);
        //         if (request.responseCode == 200)
        //         {
        //             var partyJson = response["data"]["party"];
        //             Party party = PartyManager.GetPartyFromString(partyJson.ToString());
        //             Debug.Log(party);
        //             StaticClass.CrossSceneParty = party;
        //             StaticClass.CrossSceneChatInt = party.chatId;
        //             StaticClass.CrossSceneMonster = party.monster;
        //             if (callback != null) { callback(); }
        //         }
        //     }
        // }

    }
}