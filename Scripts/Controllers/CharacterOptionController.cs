using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Barter.Classes;
using Barter.Managers;

public class CharacterOptionController : MonoBehaviour
// Responsible for the rendering of individual options
{
    // Start is called before the first frame update
    public Character character;
    public Text Name;
    //FIXME: Can add more random shit here in the future like avatar and level
    public void onClickCharacterOption()
    {
        StaticClass.SelectedCharacter = character;
        SceneManager.LoadScene("InventoryPage");
    }

}
