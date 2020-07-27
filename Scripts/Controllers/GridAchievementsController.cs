using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using Barter.Classes;
using SimpleJSON;
using Barter.Managers;
using System;

public class GridAchievementsController : MonoBehaviour
// Responsible for the control of the entire grid.
{
    public GameObject GridAchievements;
    public GameObject AchievementPrefab;


    void Start()
    {
        StartCoroutine(GetAchievements(StaticClass.SelectedCharacter, null));
    }

    public IEnumerator GetAchievements(Character character, Action callback)
    {
        foreach (Transform child in GridAchievements.transform)
        {
            // DESTROY ALL CHILDREN
            GameObject.Destroy(child.gameObject);
        }
        string url = String.Format("https://backend.josiahkhoo.me/api/characters/achievements/{0}", character.id.ToString());
        string token = string.Format("Token {0}", PlayerPrefs.GetString("token", "").ToString());
        using (var request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Authorization", token);
            yield return request.SendWebRequest();
            var response = JSON.Parse(request.downloadHandler.text);
            // Debug.Log(response);
            if (request.responseCode == 200)
            {
                print(response);
                for (int i = 0; i < response.Count; i++)
                {
                    var achievementJson = response[i];
                    GameObject achievementOption = Instantiate(AchievementPrefab) as GameObject;
                    AchievementController controller = achievementOption.GetComponent<AchievementController>();
                    controller.Title.text = achievementJson["name"];
                    controller.Value.text = achievementJson["value"];
                    controller.transform.parent = GridAchievements.transform;
                }
            }
        }
        if (callback != null)
        {
            callback();
        }
    }
}
