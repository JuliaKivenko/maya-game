using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    public GameObject canvas;
    public GameObject eventSystem;
    //public GameObject pauseUI;
    public GameObject gameOverUI;
    public GameObject winUI;
    //public GameObject mainMenu;
    public Slider slider;
    public PlayerController player;
    
    public float dashScrollSpeed;
    public float normalScrollSpeed;
    public float deceleration;
    public Text scoreText;
    public Text winScore;

    [HideInInspector]
    public float scrollSpeed;

    private int _allStars;


    void Start () 
    {
        Time.timeScale = 1;
        gameOverUI.SetActive(false);
        winUI.SetActive(false);
        _allStars = GameObject.FindGameObjectsWithTag("Ring").Length;
    }

    void Update () 
    {
        slider.value = player.energy * 0.1f;
        scoreText.text = ": " + player.score + "/" + _allStars;
	}


    public void Win ()
    {
        winScore.text = scoreText.text;
        StopAllCoroutines();
        Time.timeScale = 0;
        winUI.SetActive(true);
    }

    public void Lose ()
    {
        StopAllCoroutines();
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }


    public IEnumerator SmoothScrollSpeedChange (float value)
    {
        if (scrollSpeed > value)
        {
            while (scrollSpeed > value)
            {
                scrollSpeed -= deceleration;
                yield return null;
            }
        }

        if (scrollSpeed < value)
        {
            while (scrollSpeed < value)
            {
                scrollSpeed += 0.5f;
                yield return null;
            }
        }
    }
        
}
