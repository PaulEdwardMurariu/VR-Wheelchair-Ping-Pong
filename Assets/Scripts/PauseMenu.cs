using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public InputActionReference selectActionReference;

    [Header("Buttons")]
    public Button quitButton;
    public Button mainMenuButton;

    void Start()
    {
        // Hook the events
        quitButton.onClick.AddListener(QuitGame);
        mainMenuButton.onClick.AddListener(LoadMenu);
    }

    void Update()
    {
        if (selectActionReference)
        {
            Debug.Log("If statement 1");
            if (GameIsPaused)
            {
                Debug.Log("If statement Resume");
                Resume();
            }
            else
            {
                Debug.Log("else statement Pause");
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
  
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()

    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("startScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}