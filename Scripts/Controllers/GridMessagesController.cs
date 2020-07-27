using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using Barter.Classes;
using Barter.Managers;
using SimpleJSON;
using System;

public class GridMessagesController : MonoBehaviour
{

    public User user;
    public User otherUser;
    public int conversationId;
    public int chatId;
    public GameObject GridMessages;

    public GameObject MessagePrefab;

    public List<Message> messages;

    public List<Message> newMessages;

    void Start()
    {
        foreach (Transform child in GridMessages.transform)
        {
            // DESTROY ALL CHILDREN
            GameObject.Destroy(child.gameObject);
        }
        messages = new List<Message>();
        user = StaticClass.CrossSceneUser;
        chatId = StaticClass.CrossSceneChatInt;
        StartCoroutine(GetChats(null));
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {

    }

    IEnumerator GetChats(Action callback)
    {
        if (StaticClass.CrossSceneChatInt == 0)
        {
            yield return new WaitForSeconds(5);
            StartCoroutine(GetChats(null));
        }

        string token = string.Format("Token {0}", PlayerPrefs.GetString("token", "").ToString());
        print(token);
        using (var request = UnityWebRequest.Get(String.Format("https://backend.josiahkhoo.me/api/chats/{0}", chatId)))
        {

            request.SetRequestHeader("Authorization", token);
            yield return request.SendWebRequest();
            var response = JSON.Parse(request.downloadHandler.text);
            if (request.responseCode == 200)
            {
                newMessages = new List<Message>();
                var messagesArray = response["data"]["chat"]["messages"];
                for (int i = 0; i < messagesArray.Count; i++)
                {
                    var messageJson = messagesArray[i];
                    Message message = MessageManager.GetMessageFromString(messageJson.ToString());
                    if (!(messages.Contains(message)))
                    {
                        newMessages.Add(message);
                        messages.Add(message);
                    }
                }
            }
        }
        foreach (Message message in newMessages)
        {
            GameObject friendOption = Instantiate(MessagePrefab) as GameObject;
            MessageOptionController controller = friendOption.GetComponent<MessageOptionController>();
            // modify controller text here
            controller.Name.text = message.username;
            controller.Content.text = message.content;
            controller.Datetime.text = message.datetimeCreated.TimeOfDay.ToString();
            controller.transform.parent = GridMessages.transform;
        }
        // RECURSION
        yield return new WaitForSeconds(1);
        StartCoroutine(GetChats(null));
    }

}