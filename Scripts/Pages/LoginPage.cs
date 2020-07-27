using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;
using Barter.Managers;
using Barter.Classes;


using BayatGames.SaveGameFree;

public class LoginPage : MonoBehaviour
{
    public GameObject username;
    public GameObject password;
    private string username_text;
    private string password_text;
    public GameObject wrongToast;

    public void LoginBtn()
    {
        if (password_text != "" && username_text != "")
        {
            StartCoroutine(Login(username_text, password_text));
        }
    }

    public void SignUpBtn()
    {
        SceneManager.LoadScene("SignupPage");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
        }
        username_text = username.GetComponent<InputField>().text;
        password_text = password.GetComponent<InputField>().text;
    }

    // Starts a coroutine login process
    IEnumerator Login(String username, String password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        using (var request = UnityWebRequest.Post("https://backend.josiahkhoo.me/api/users/login/", form))
        {
            yield return request.SendWebRequest();
            Debug.Log($"Response: {request.downloadHandler.text}");
            var response = JSON.Parse(request.downloadHandler.text);
            if (request.responseCode == 200)
            {
                var token = response["token"];
                var user = response["data"]["user"];
                StaticClass.CrossSceneUser = UserManager.InstantiateUser(user.ToString());
                PlayerPrefs.SetString("token", token);

                SceneManager.LoadScene("CharactersPage");
            }
            else
            {
                print("sry bro");
                wrongToast.SetActive(true);
            }
        }
    }

}
