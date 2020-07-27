using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using Barter.Classes;
using Barter.Managers;
using SimpleJSON;
using BayatGames.SaveGameFree;

public class GridFriendOptionsController : MonoBehaviour
{
    public GameObject GridFriendOptions;
    public GameObject FriendOptionPrefab;

    void Start()
    {
        StartCoroutine(GetFriends());
    }

    IEnumerator GetFriends()
    {
        List<User> users = new List<User>();
        string token = string.Format("Token {0}", PlayerPrefs.GetString("token", "").ToString());
        print(token);
        using (var request = UnityWebRequest.Get("https://backend.josiahkhoo.me/api/users/friends/"))
        {

            request.SetRequestHeader("Authorization", token);
            yield return request.SendWebRequest();
            var response = JSON.Parse(request.downloadHandler.text);
            if (request.responseCode == 200)
            {
                var usersArray = response["users"];
                for (int i = 0; i < usersArray.Count; i++)
                {
                    var userJson = usersArray[i];
                    User user = UserManager.GetUserFromString(userJson.ToString());
                    print(user);
                    users.Add(user);
                }
            }
        }
        print(users.Count);
        foreach (User user in users)
        {
            GameObject friendOption = Instantiate(FriendOptionPrefab) as GameObject;
            FriendOptionController controller = friendOption.GetComponent<FriendOptionController>();
            // modify controller text here
            controller.user = user;
            controller.Name.text = user.username;
            print(controller.Name.text);
            controller.transform.parent = GridFriendOptions.transform;
        }
    }
}