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

public class FriendOptionController : MonoBehaviour
// Responsible for the rendering of individual options
{
    // Start is called before the first frame update
    public User user;
    public Text Name;
    //FIXME: Can add more random shit here in the future like avatar and level


    public void OnClickFriend()
    {
        StartCoroutine(UserManager.GetConversation(user.id, () =>
        {
            StaticClass.CrossSceneOtherUser = user;
            SceneManager.LoadScene("ChatPage");
        }));
    }

}