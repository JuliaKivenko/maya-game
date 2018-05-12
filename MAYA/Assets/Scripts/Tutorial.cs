using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tutorial : MonoBehaviour {

    public GameObject tutorial1;
    public GameObject tutorial2;
    private PlayerController _player;
    private bool _displayDashTutorial = true;
    private bool _displayFlyTutorial = true;

    

    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    
    
    void Update()
    {
        //Check if player has already seen tutorial
        int tutorialSeen = PlayerPrefs.GetInt("tutorialSeen");
        if (tutorialSeen == 1)
        {
            tutorial1.SetActive(false);
            return;
        }


        //Display first part of tutorial
        if (_displayFlyTutorial)
        {
            Time.timeScale = 0;
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                Time.timeScale = 1;
                tutorial1.SetActive(false);
                _displayFlyTutorial = false;

            }

        }

        if (tutorial1.activeSelf == true)
        {
            Time.timeScale = (_displayFlyTutorial) ? 0 : 1;
        }


        //Display Dash tutorial
        if (_player.energy <= 5 && _displayDashTutorial == true)
        {
            Time.timeScale = 0;
            tutorial2.SetActive(true);
            if (Input.GetButton("Dash"))
            {

                Time.timeScale = 1;

                tutorial2.SetActive(false);
                _displayDashTutorial = false;
                PlayerPrefs.SetInt("tutorialSeen", 1);
            }
        }
    }
}
