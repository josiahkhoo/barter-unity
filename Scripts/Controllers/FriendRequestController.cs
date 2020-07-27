using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Barter.Classes;
using Barter.Managers;
using UnityEngine.Networking;
using UnityEngine.Events;
using System;

public class FriendRequestController : MonoBehaviour
// Responsible for the rendering of individual options
{
    // Start is called before the first frame update
    public GameObject FriendRequestPrefab;
    public User user;
    public Text Name;
    //FIXME: Can add more random shit here in the future like avatar and level


    public void OnClickAddFriend()
    {
        StartCoroutine(UserManager.AddFriend(user.username, () =>
        {
            GameObject.Destroy(FriendRequestPrefab.gameObject);
        }));
    }

    public void OnClickDeclineFriend()
    {
        StartCoroutine(UserManager.DeclineFriend(user, () =>
        {
            GameObject.Destroy(FriendRequestPrefab.gameObject);
        }));
    }

}