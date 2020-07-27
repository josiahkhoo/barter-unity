using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SocialPageGuild : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
