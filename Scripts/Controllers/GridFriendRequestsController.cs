using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using Barter.Classes;
using Barter.Managers;
using SimpleJSON;
using BayatGames.SaveGameFree;

public class GridFriendRequestsController : MonoBehaviour
{
    public GameObject GridFriendRequests;

    public GameObject FriendRequestPrefab;


    void Start()
    {
        StartCoroutine(GetFriendRequests());
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        StartCoroutine(GetFriendRequests());
    }

    IEnumerator GetFriendRequests()
    {
        foreach (Transform child in GridFriendRequests.transform)
        {
            // DESTROY ALL CHILDREN
            GameObject.Destroy(child.gameObject);
        }

        List<User> users = new List<User>();
        string token = string.Format("Token {0}", PlayerPrefs.GetString("token", "").ToString());
        print(token);
        using (var request = UnityWebRequest.Get("https://backend.josiahkhoo.me/api/users/friends/requests/"))
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
            GameObject friendRequest = Instantiate(FriendRequestPrefab) as GameObject;
            FriendRequestController controller = friendRequest.GetComponent<FriendRequestController>();
            // modify controller text here
            controller.user = user;
            controller.Name.text = user.username;
            print(controller.Name.text);
            controller.transform.parent = GridFriendRequests.transform;
        }
    }
}