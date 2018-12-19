using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAppearScript : MonoBehaviour
{
    public GameObject menu;
    private bool isShowing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }
    }
}
