using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class RealVRWPPTutorialPause : MonoBehaviour
{
    public InputActionReference pauseButton;

    // public MeshRenderer mesh;
    public static bool GameIsPaused = false;

    [Header("Screens")]
    public GameObject pauseMenuUI;
    public GameObject ControlsUI;

    [Header("Return To Menu Screen")]
    public GameObject MenuMessage;

    [Header("Pause Menu Buttons")]
    public Button ResumeUI;
    public Button MainMenuUI;
    public Button ControlsButtonUI;

    [Header("Control Menu Return Button")]
    public Button ControlsReturnUI;


    [Header("Hands And Rays")]
    // gameobjects for hands to disappear
    public GameObject leftHand;
    public GameObject rightHand;

    public GameObject leftRayInteractor;
    public GameObject rightRayInteractor;


    // Start is called before the first frame update
    void Start()
    {
        //GameIsPaused = false;
        pauseMenuUI.SetActive(false);
        ControlsUI.SetActive(false);

        pauseButton.action.started += PausePressed;
        //pauseButton.action. += PauseReleased;

        // hook events for buttons
        ResumeUI.onClick.AddListener(ResumePressed);
        MainMenuUI.onClick.AddListener(MainMenuPressed);
        ControlsButtonUI.onClick.AddListener(ControlButtonPressed);
        ControlsReturnUI.onClick.AddListener(ControlReturnButtonPressed);

        // hands
        leftHand.SetActive(true);
        rightHand.SetActive(true);

        leftRayInteractor.SetActive(false);
        rightRayInteractor.SetActive(false);

        GameIsPaused = false;
        MenuMessage.SetActive(false);


    }

    public void ControlReturnButtonPressed()
    {
        pauseMenuUI.SetActive(true);
        ControlsUI.SetActive(false);
    }

    public void ControlButtonPressed()
    {
        pauseMenuUI.SetActive(false);
        ControlsUI.SetActive(true);
    }

    void PausePressed(InputAction.CallbackContext context)
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;

        // deactivate left hand
        leftHand.SetActive(false);

        // deactivate right hand
        rightHand.SetActive(false);

        leftRayInteractor.SetActive(true);
        rightRayInteractor.SetActive(true);



    }

    public void ResumePressed()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;

        // activate left hand
        leftHand.SetActive(true);

        // activate right hand
        rightHand.SetActive(true);

        leftRayInteractor.SetActive(false);
        rightRayInteractor.SetActive(false);


    }

    public void MainMenuPressed()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        MenuMessage.SetActive(true);


        string startingScene = "startScene";

        //Debug.Log("REACHED SWITCH POINT");


        //Launch the new scene
        SceneTransitionManager.singleton.GoToSceneAsync(startingScene);
        //MenuMessage.SetActive(false);

        // activate left hand
        leftHand.SetActive(true);

        // activate right hand
        rightHand.SetActive(true);

        //Debug.Log("PASSED SWITCH POINT");

    }


    // Update is called once per frame
    void Update()
    {
        //if ()

    }
}