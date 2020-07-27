using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public enum PasswordScore
{
    Blank = 0,
    VeryWeak = 1,
    Weak = 2,
    Medium = 3,
    Strong = 4,
    VeryStrong = 5
}

public class SignupPage : MonoBehaviour
{
    private int PasswordStrength;
    public GameObject username;
    public GameObject email;
    public GameObject password;
    private string username_text;
    private string email_text;
    private string password_text;
    public GameObject pwStxt;
    //private bool emailValid = false;

    public void SignupBtn()
    {

        if (password_text != "" && username_text != "" && email_text != "")
        {
            //send to data base to get token
            //enter token
            if (PasswordStrength >= 2)
            {
                StartCoroutine(Signup(email_text, username_text, password_text));
            }
        }
    }

    public void toLogin()
    {
        SceneManager.LoadScene("LoginPage");
    }


    // Start is called before the first frame update
    void Start()
    {
        pwStxt.SetActive(false);
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
            else if (email.GetComponent<InputField>().isFocused)
            {
                username.GetComponent<InputField>().Select();
            }
        }

        email_text = email.GetComponent<InputField>().text;
        username_text = username.GetComponent<InputField>().text;
        password_text = password.GetComponent<InputField>().text;

        PasswordStrength = CheckStrength(password_text);
        pwStxt.SetActive(true);
        switch (PasswordStrength)
        {
            case 0:
                pwStxt.GetComponent<UnityEngine.UI.Text>().text = "Password Blank";
                break;
            case 1:
                pwStxt.GetComponent<UnityEngine.UI.Text>().text = "Strength: VeryWeak";
                break;
            case 2:
                pwStxt.GetComponent<UnityEngine.UI.Text>().text = "Strength: Weak";
                break;
            // Show an error message to the user
            case 3:
                pwStxt.GetComponent<UnityEngine.UI.Text>().text = "Strength: Medium";
                break;
            case 4:
                pwStxt.GetComponent<UnityEngine.UI.Text>().text = "Strength: Strong";
                break;
            case 5:
                pwStxt.GetComponent<UnityEngine.UI.Text>().text = "Strength: Very Strong";
                break;
                // Password deemed strong enough, allow user to be added to database etc
        }

    }



    public int CheckStrength(string password)
    {
        int score = 0;

        if (password.Length < 1)
            return 0;
        if (password.Length < 4)
            return 1;

        if (password.Length >= 8)
            score++;
        if (password.Length >= 12)
            score++;
        if (password.Any(char.IsDigit))
            score++;
        if (password.Any(char.IsUpper))
            score++;
        if (password.Any(char.IsLower))
            score++;

        return score;
    }

    IEnumerator Signup(String email, String username, String password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password1", password);
        form.AddField("email", email);
        using (var request = UnityWebRequest.Post("https://backend.josiahkhoo.me/api/users/create/", form))
        {
            yield return request.SendWebRequest();
            // Debug.Log($"Response: {request.downloadHandler.text}");
            if (request.responseCode == 200)
            {
                SceneManager.LoadScene("LoginPage");
            }
        }
    }
}