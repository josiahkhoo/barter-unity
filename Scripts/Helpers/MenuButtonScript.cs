using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject menu;

    public void menuButtonOnClick() {
        if(menu.activeSelf) {
            this.menu.SetActive(false);
        } else {
            this.menu.SetActive(true);
        }
        
    }

    void Start()
    {
        this.menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
