using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Barter.Classes;
using Barter.Managers;

public class ChatPage : MonoBehaviour
{
    public User otherUser;
    public User user;
    public GameObject message;
    public GameObject nameText;
    public int chatId;
    private string message_text;

    public void toInventory()
    {
        SceneManager.LoadScene("InventoryPage");
    }

    public void toFriendsTab()
    {
        SceneManager.LoadScene("SocialPageFriends");
    }

    public void toQuestPage()
    {
        SceneManager.LoadScene("QuestPage");
    }

    public void sendMessage()
    {
        //get username here
        //send
        print("<" + message_text + ">" + " sent");
        StartCoroutine(UserManager.SendMessage(chatId, message_text, () =>
        {
            message.GetComponent<InputField>().text = "";
        }));
    }
    // Start is called before the first frame update
    void Start()
    {
        this.otherUser = StaticClass.CrossSceneOtherUser;
        this.user = StaticClass.CrossSceneUser;
        this.chatId = StaticClass.CrossSceneChatInt;
        this.nameText.GetComponent<Text>().text = otherUser.username;
    }

    // Update is called once per frame
    void Update()
    {
        message_text = message.GetComponent<InputField>().text;
    }
}
