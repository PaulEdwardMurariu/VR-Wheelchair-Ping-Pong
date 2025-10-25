using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealGameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject about;
    public GameObject controls;

    [Header("Main Menu Buttons")]
    public Button tutorialButton;
    public Button twoVStwoButton;
    public Button oneVSoneButton;
    public Button aboutButton;
    public Button quitButton;
    public Button controlsButton;

    [Header("About Button")]
    public Button aboutReturnButton;

    [Header("Controls Button")]
    public Button controlsReturnButton;

    //public List<Button> returnButtons;

    // Start is called before the first frame update
    void Start()
    {
        EnableMainMenu();

        //Hook events
        oneVSoneButton.onClick.AddListener(Start1V1Game);
        twoVStwoButton.onClick.AddListener(Start2V2Game);
        tutorialButton.onClick.AddListener(StartTutorial);
        aboutButton.onClick.AddListener(EnableAbout);
        quitButton.onClick.AddListener(QuitGame);
        aboutReturnButton.onClick.AddListener(AboutReturnToMenu);
        controlsButton.onClick.AddListener(EnableControls);
        controlsReturnButton.onClick.AddListener(ControlsReturnToMenu);

        //foreach (var item in returnButtons)
        //{
        //    item.onClick.AddListener(EnableMainMenu);
        //}
    }

    public void EnableControls()
    {
        mainMenu.SetActive(false);
        controls.SetActive(true);
    }

    public void ControlsReturnToMenu()
    {
        mainMenu.SetActive(true);
        controls.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit dont quit noodles!");
    }

    public void Start1V1Game()
    {
        string startingScene = "VRWCPP_1v1";

        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(startingScene);
    }
    public void Start2V2Game()
    {
        string startingScene = "VRWCPP_2v2";

        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(startingScene);
    }

    public void StartTutorial()
    {
        string tutorialScene = "VRWCPP_Tutorial";

        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(tutorialScene);
    }


    public void HideAll()
    {
        mainMenu.SetActive(false);
        about.SetActive(false);
        controls.SetActive(false);
    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        about.SetActive(false);
        controls.SetActive(false);
    }
    public void EnableOption()
    {
        mainMenu.SetActive(false);
        about.SetActive(false);
    }
    public void EnableAbout()
    {
        mainMenu.SetActive(false);
        about.SetActive(true);
    }

    public void AboutReturnToMenu()
    {
        mainMenu.SetActive(true);
        about.SetActive(false);
    }
}
