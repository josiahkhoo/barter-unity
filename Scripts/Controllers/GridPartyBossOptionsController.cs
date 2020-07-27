using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using Barter.Classes;
using Barter.Managers;
using SimpleJSON;
using Assets.HeroEditor.Common.CharacterScripts;

public class GridPartyBossOptionsController : MonoBehaviour
// Responsible for the control of the entire grid.
{
    public GameObject GridBossOptions;
    public GameObject BossOptionPrefab;
    void Start()
    {
        StartCoroutine(GetPartyMonsters());
    }

    IEnumerator GetPartyMonsters()
    {
        List<Monster> monsters = new List<Monster>();
        using (var request = UnityWebRequest.Get("https://backend.josiahkhoo.me/api/monsters/multiplayer"))
        {
            yield return request.SendWebRequest();
            var response = JSON.Parse(request.downloadHandler.text);
            if (request.responseCode == 200)
            {
                print(response);
                var monstersArray = response["data"];
                for (int i = 0; i < monstersArray.Count; i++)
                {
                    var monsterJson = monstersArray[i];
                    Monster monster = MonsterManager.GetMonsterFromString(monsterJson.ToString());
                    monsters.Add(monster);
                }
            }
        }
        foreach (Monster monster in monsters)
        {
            GameObject bossOption = Instantiate(BossOptionPrefab) as GameObject;
            PartyBossOptionController controller = bossOption.GetComponent<PartyBossOptionController>();
            controller.monster = monster;
            controller.Name.text = monster.name;
            // controller.Level.text = string.Format("Lvl: {0}", monster.level.ToString());
            controller.PartySize.text = string.Format("Size: {0}", monster.partySize.ToString());
            controller.Duration.text = string.Format("{0} minutes", (monster.duration / 60).ToString());
            // sets parent of the character option to the grid
            controller.transform.parent = GridBossOptions.transform;
        }
    }
}
