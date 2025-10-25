using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class VRWPPPause : MonoBehaviour
{
    public InputActionReference pauseButton;

    // public MeshRenderer mesh;
    public static bool GameIsPaused = false;

    [Header("Menu Screen")]
    public GameObject pauseMenuUI;

    [Header("Return To Menu Screen")]
    public GameObject MenuMessage;

    [Header("Pause Menu Buttons")]
    public Button resumeUI;
    public Button MainMenuUI;

    [Header("Hands And Rays")]
    // gameobjects for hands to disappear
    public GameObject leftHand;
    public GameObject rightHand;


    // Start is called before the first frame update
    void Start()
    {
        //GameIsPaused = false;
        pauseMenuUI.SetActive(false);

        pauseButton.action.started += PausePressed;
        //pauseButton.action. += PauseReleased;

        // hook events for buttons
        resumeUI.onClick.AddListener(ResumePressed);
        MainMenuUI.onClick.AddListener(MainMenuPressed);

        // hands
        leftHand.SetActive(true);
        rightHand.SetActive(true);

        GameIsPaused = false;
        MenuMessage.SetActive(false);


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
