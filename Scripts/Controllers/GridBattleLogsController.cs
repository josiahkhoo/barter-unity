using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;
using Barter.Classes;
using Barter.Managers;
using SimpleJSON;

public class GridBattleLogsController : MonoBehaviour
// Responsible for the control of the entire grid.
{
    List<Battle> battles;
    public GameObject GridBattleLogsOptions;
    public GameObject battleLogPrefab;

    void Start()
    {
        StartCoroutine(GetBattlesAndRenderLogs());
    }

    public IEnumerator GetBattlesAndRenderLogs()
    {
        battles = new List<Battle>();
        string url = string.Format("https://backend.josiahkhoo.me/api/battles?character_id={0}", StaticClass.SelectedCharacter.id.ToString());
        string token = string.Format("Token {0}", PlayerPrefs.GetString("token", "").ToString());
        using (var request = UnityWebRequest.Get(url))
        {
            // Debug.Log(url);
            request.SetRequestHeader("Authorization", token);
            yield return request.SendWebRequest();
            var response = JSON.Parse(request.downloadHandler.text);
            // Debug.Log(response);
            if (request.responseCode == 200)
            {
                var battlesArray = response["battles"];
                for (int i = 0; i < battlesArray.Count; i++)
                {
                    var battleJson = battlesArray[i];
                    Battle battle = BattleManager.GetBattleFromString(battleJson.ToString());
                    battles.Add(battle);
                    print(battle.id);
                }
            }

        }
        foreach (Battle thisBattle in battles)
        {
            GameObject battleLogOption = Instantiate(battleLogPrefab) as GameObject;
            BattleLogController controller = battleLogOption.GetComponent<BattleLogController>();
            controller.BossName.text = string.Format("Boss: {0}", thisBattle.monster.name.ToString());
            controller.BossTime.text = string.Format("Date: {0}", thisBattle.datetimeCreated.ToShortDateString());
            if (thisBattle.equipment != null)
            {
                controller.drop.text = string.Format("Drop: {0}", thisBattle.equipment.name.ToString());
            }
            else
            {
                controller.drop.text = "";
            }
            if (thisBattle.state == 1)
            {
                controller.state.text = string.Format("Status: Win");
            }
            else
            {
                controller.state.text = string.Format("Status: Failed");
            }
            // sets parent of the character option to the grid
            controller.transform.parent = GridBattleLogsOptions.transform;
        }
    }
}
