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
    public static class UserManager
    {
        public static User GetUserFromString(string userString)
        {
            var userJson = JSON.Parse(userString);
            var charactersJson = userJson["characters"];
            List<Character> characters = new List<Character>();
            for (int i = 0; i < charactersJson.Count; i++)
            {
                var characterJson = charactersJson[i];
                Character character = CharacterManager.GetCharacterFromString(characterJson.ToString());
                characters.Add(character);
            }
            int userId = userJson["id"];
            string email = userJson["email"];

            int status = userJson["status"];
            string username = userJson["username"];

            return new User(userId, username, email, status, characters);

        }
        public static User InstantiateUser(string userString)
        {
            Storage.User = GetUserFromString(userString);
            return Storage.User;
        }

        public static IEnumerator RefreshUser(int userId)
        {
            string url = string.Format("https://backend.josiahkhoo.me/api/users/{0}", userId);
            using (var request = UnityWebRequest.Get(url))
            {
                yield return request.SendWebRequest();
                var response = JSON.Parse(request.downloadHandler.text);
                // Debug.Log(response);
                if (request.responseCode == 200)
                {
                    var user = response["data"]["user"];
                    UserManager.InstantiateUser(user.ToString());
                }
            }
        }

        public static IEnumerator AddFriend(string username, Action callback)
        {
            string url = string.Format("https://backend.josiahkhoo.me/api/users/friends/{0}", username);
            string token = string.Format("Token {0}", PlayerPrefs.GetString("token", "").ToString());
            WWWForm form = new WWWForm();
            using (var request = UnityWebRequest.Post(url, form))
            {
                request.SetRequestHeader("Authorization", token);
                yield return request.SendWebRequest();
                var response = JSON.Parse(request.downloadHandler.text);
                // Debug.Log(response);
                if (request.responseCode == 200)
                {
                }
                if (callback != null) { callback(); }
            }
        }

        public static IEnumerator DeclineFriend(User otherUser, Action callback)
        {
            string url = "https://backend.josiahkhoo.me/api/users/friends/decline/";
            string token = string.Format("Token {0}", PlayerPrefs.GetString("token", "").ToString());
            WWWForm form = new WWWForm();
            form.AddField("user", otherUser.id);
            using (var request = UnityWebRequest.Post(url, form))
            {
                request.SetRequestHeader("Authorization", token);
                yield return request.SendWebRequest();
                var response = JSON.Parse(request.downloadHandler.text);
                // Debug.Log(response);
                if (request.responseCode == 200)
                {
                }
                if (callback != null) { callback(); }
            }
        }

        public static IEnumerator GetConversation(int otherUserId, Action callback)
        {
            string url = string.Format("https://backend.josiahkhoo.me/api/chats/user/{0}", otherUserId.ToString());
            string token = string.Format("Token {0}", PlayerPrefs.GetString("token", "").ToString());
            using (var request = UnityWebRequest.Get(url))
            {
                request.SetRequestHeader("Authorization", token);
                yield return request.SendWebRequest();
                var response = JSON.Parse(request.downloadHandler.text);
                // Debug.Log(response);
                if (request.responseCode == 200)
                {
                    int conversationId = response["data"]["conversation"]["id"];
                    int chatId = response["data"]["conversation"]["chat"];
                    StaticClass.CrossSceneConversationInt = conversationId;
                    StaticClass.CrossSceneChatInt = chatId;
                }
                if (callback != null) { callback(); }
            }
        }

        public static IEnumerator SendMessage(int chatId, string content, Action callback)
        {
            string url = string.Format("https://backend.josiahkhoo.me/api/chats/{0}/message/", chatId);
            string token = string.Format("Token {0}", PlayerPrefs.GetString("token", "").ToString());
            WWWForm form = new WWWForm();
            form.AddField("content", content);
            using (var request = UnityWebRequest.Post(url, form))
            {
                request.SetRequestHeader("Authorization", token);
                yield return request.SendWebRequest();
                var response = JSON.Parse(request.downloadHandler.text);
                // Debug.Log(response);
                if (request.responseCode == 200)
                {
                }
                if (callback != null) { callback(); }
            }
        }
    }
}
