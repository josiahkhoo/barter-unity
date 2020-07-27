using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public void toBattleLogs() {
        SceneManager.LoadScene("BattleLogsPage");
    }

    public void toAchievements() {
        SceneManager.LoadScene("AchievementsPage");
    }

    public void logoutButton() {
        print("byeeeee");
        //wipe cache here
        SceneManager.LoadScene("LoginPage");
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
