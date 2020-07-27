using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using Barter.Managers;

public class SocialPageFriends : MonoBehaviour
{
    public GameObject addFriendBox;

    public GameObject friendRequestBox;

    public GameObject friendName;

    public string friendName_text;



    public void toChat()
    {
        SceneManager.LoadScene("ChatPage");
    }

    public void toGuildTab()
    {
        SceneManager.LoadScene("SocialPageGuild");
    }

    public void toInventory()
    {
        SceneManager.LoadScene("InventoryPage");
    }

    public void toFriendsTab()
    {
        SceneManager.LoadScene("SocialPageFriends");
    }

    public void addFriend()
    {
        StartCoroutine(UserManager.AddFriend(friendName_text, () =>
        {
            this.addFriendBox.SetActive(false);
            friendName.GetComponent<InputField>().text = "";
        }));

    }

    public void toQuestPage()
    {
        SceneManager.LoadScene("QuestPage");
    }

    public void addFriendButton()
    {
        this.addFriendBox.SetActive(true);
    }

    public void closeAddFriend()
    {
        this.addFriendBox.SetActive(false);
    }

    public void openFriendRequest()
    {
        this.friendRequestBox.SetActive(true);
    }

    public void closeFriendRequest()
    {
        this.friendRequestBox.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.addFriendBox.SetActive(false);
        this.friendRequestBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        friendName_text = friendName.GetComponent<InputField>().text;
    }
}
