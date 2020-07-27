using UnityEngine;
using System;

namespace Barter.Classes
{

    public class Message
    {
        public string id;
        public string content;
        public int messageType;
        public int userId;

        public string username;
        public DateTime datetimeCreated;

        public Message(string id, string content, int messageType, int userId, string username, DateTime datetimeCreated)
        {
            this.id = id;
            this.content = content;
            this.messageType = messageType;
            this.userId = userId;
            this.username = username;
            this.datetimeCreated = datetimeCreated;
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
                return false;
            Message c = obj as Message;
            if ((System.Object)c == null)
                return false;
            return id == c.id;
        }

        public bool Equals(Message c)
        {
            if ((object)c == null)
                return false;
            return id == c.id;
        }


    }

}