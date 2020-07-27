using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine.UI;
using Barter.Managers;
using UnityEngine.SceneManagement;
using Barter.Classes;

public class FightGameState : MonoBehaviour
{
    // Start is called before the first frame update
    public Assets.HeroEditor.Common.CharacterScripts.Character character;
    public GameObject CharacterObject;
    public Image monster;
    public GameObject winningText;
    public Image currentHealth;
    public Text counter;
    float timeLeft;
    float maxTime = 5f;
    bool fighting;
    bool victory;
    public Button fightButton;
    public Button runButton;
    public Button exitButton;
    void Start()
    {
        maxTime = (float)StaticClass.CrossSceneMonster.duration;
        CharacterManager.InstantiateCharacterObject(CharacterObject, null);
        // this.character = GetComponent<Character>();
        // this.currentHealth = GetComponent<Image>();
        this.timeLeft = maxTime;
        this.fighting = false;
        this.victory = false;
        // this.counter = GetComponent<Text>();
        // this.fightButton = GetComponent<Button>();
        // this.runButton = GetComponent<Button>();
        this.exitButton.gameObject.SetActive(false);
        this.winningText.SetActive(false);
        this.fightButton.onClick.AddListener(FightButtonOnClick);
        this.exitButton.onClick.AddListener(ExitButtonOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.fighting)
        {
            this.Fight();
        }
    }

    void Fight()
    {
        this.fightButton.gameObject.SetActive(false);
        this.runButton.gameObject.SetActive(false);
        if (timeLeft > 0)
        {
            this.timeLeft -= Time.deltaTime;
            this.currentHealth.fillAmount = timeLeft / maxTime;
            this.counter.text = timeLeft.ToString();
            character.Animator.SetTrigger(Time.frameCount % 2 == 0 ? "Slash" : "Jab");
        }
        else
        {
            this.fighting = false;
            this.EndFight();
        }
    }

    void EndFight()
    {
        this.monster.enabled = false;
        this.winningText.SetActive(true);
        this.victory = true;
        this.counter.text = "";
        StartCoroutine(BattleManager.completeBattle(StaticClass.CrossSceneBattle, () =>
        {
            this.exitButton.gameObject.SetActive(true);
        }));
    }

    void FightButtonOnClick()
    {
        this.fighting = true;
        this.Fight();
    }

    // Event handler for exit button click (modify to leave this scene
    // in the future)
    void ExitButtonOnClick()
    {
        //this.fighting = false;
        //this.victory = false;
        // this.exitButton.gameObject.SetActive(false);
        // this.fightButton.gameObject.SetActive(true);
        // this.runButton.gameObject.SetActive(true);
        // this.monster.SetActive(true);
        //this.currentHealth.fillAmount = 1;
        //this.timeLeft = maxTime;
        toRewardsPage();
    }
    public void toRewardsPage()
    {
        SceneManager.LoadScene("RewardsPage");
    }
    public void toQuestPage()
    {
        SceneManager.LoadScene("QuestPage");
    }
}
