using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;
using Barter.Classes;

public class GridCharacterOptionsController : MonoBehaviour
// Responsible for the control of the entire grid.
{
    public GameObject GridCharacterOptions;
    public GameObject CharacterOptionPrefab;
    void Start()
    {
        foreach (Character character in Storage.User.characters)
        {
            GameObject characterOption = Instantiate(CharacterOptionPrefab) as GameObject;
            CharacterOptionController controller = characterOption.GetComponent<CharacterOptionController>();
            controller.character = character;
            controller.Name.text = character.name;
            // sets parent of the character option to the grid
            controller.transform.parent = GridCharacterOptions.transform;
        }
    }
}
