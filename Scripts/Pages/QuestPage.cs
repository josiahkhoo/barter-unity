using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Barter.Managers;

public class QuestPage : MonoBehaviour
{
    public GameObject partyCode;

    public GameObject errorMessage;

    private string partyCode_text;

    public GameObject joinPartyBox;

    public void toInventory()
    {
        SceneManager.LoadScene("InventoryPage");
    }

    public void toFriendsTab()
    {
        SceneManager.LoadScene("SocialPageFriends");
    }

    public void joinPartyButton()
    {
        this.joinPartyBox.SetActive(true);
    }

    public void closeJoinParty()
    {
        this.joinPartyBox.SetActive(false);
    }
    public void joinParty()
    {
        StartCoroutine(PartyManager.joinParty(partyCode_text, () =>
        {
            print("<" + partyCode_text + ">" + " sent");
            partyCode.GetComponent<InputField>().text = "";
            SceneManager.LoadScene("PartyPage");
        }, () => {
            errorToggle();
        }));
    }

    public void errorToggle()
    {
            this.errorMessage.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.joinPartyBox.SetActive(false);
        this.errorMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        partyCode_text = partyCode.GetComponent<InputField>().text.ToUpper();
    }
}
