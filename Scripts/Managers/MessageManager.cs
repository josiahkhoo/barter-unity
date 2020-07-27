using SimpleJSON;
using System.Collections.Generic;
using System.Collections;
using Barter.Classes;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using System;

namespace Barter.Managers
{
    public static class MessageManager
    {
        public static Message GetMessageFromString(string messageString)
        {

            var messageJson = JSON.Parse(messageString);
            string id = messageJson["id"];
            string content = messageJson["content"];
            int messageType = messageJson["message_type"];
            int userId = messageJson["user"];
            string userName = messageJson["username"];
            DateTime datetimeCreated = DateTime.Parse(messageJson["datetime_created"]);

            return new Message(id, content, messageType, userId, userName, datetimeCreated);

        }
    }
}