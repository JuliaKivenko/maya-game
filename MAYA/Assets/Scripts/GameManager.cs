using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    [Header("UI Elements")]
    public GameObject canvas;
    public GameObject eventSystem;
    public GameObject gameOverUI;
    public GameObject winUI;
    public Slider slider;
    
    [Header("Player Controller")]
    public PlayerController player;
    
    [Header("Level Scroll Speed Variables")]
    public float dashScrollSpeed;
    public float normalScrollSpeed;
    public float deceleration;
    
    [Header("Score Texts")]
    public Text scoreText;
    public Text winScore;

    [HideInInspector]
    public float scrollSpeed;

    private int _totalStars;




    void Start () 
    {
        Time.timeScale = 1;
        gameOverUI.SetActive(false);
        winUI.SetActive(false);
        _totalStars = GameObject.FindGameObjectsWithTag("Ring").Length;
    }



    void Update () 
    {
        slider.value = player.energy * 0.1f;
        scoreText.text = ": " + player.score + "/" + _totalStars;
	}



    //Displays Win UI when player successfully completes the level
    public void Win ()
    {
        winScore.text = scoreText.text;
        StopAllCoroutines();
        Time.timeScale = 0;
        winUI.SetActive(true);
    }

    
    
    //Displays Lose UI when player loses
    public void Lose ()
    {
        StopAllCoroutines();
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
    }

    
    
    //Restarts level
    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    
    
    //Smooth transition between different level scroll values 
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
