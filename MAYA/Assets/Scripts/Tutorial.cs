using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    public GameObject tutorial1;
    public GameObject tutorial2;
    private PlayerController _player;
    private bool _displayDashTutorial = true;
    private bool _displayFlyTutorial = true;
    // Use this for initialization

    //Player preferences to save the value to show tutorial only once and never again

    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
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

        if (_player.energy <= 5 && _displayDashTutorial == true)
        {
            Time.timeScale = 0;
            tutorial2.SetActive(true);
            if (Input.GetButton("Dash"))
            {

                Time.timeScale = 1;
                tutorial2.SetActive(false);
                _displayDashTutorial = false;
            }
        }
    }
}
