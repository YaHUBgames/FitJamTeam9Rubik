using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenuScript : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject menu;
    public GameObject loseScreen;
    public GameObject winScreen;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Continue();
            else
                Pause();
        }
    }

    public void Continue()
    {
        Time.timeScale = 1;
        isPaused = false;
        menu.SetActive(false);
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        isPaused = true;
        menu.SetActive(true);
    }

    public void Reload()
    {
        SceneManager.LoadScene(1);
    }

    public void Lose()
    {
        Time.timeScale = 0;
        isPaused = true;
        loseScreen.SetActive(true);
    }

    public void win()
    {
        Time.timeScale = 0;
        isPaused = true;
        winScreen.SetActive(true);
    }
}