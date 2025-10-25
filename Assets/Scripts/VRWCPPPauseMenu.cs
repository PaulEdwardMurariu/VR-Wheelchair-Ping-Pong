using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using CommonUsages = UnityEngine.XR.CommonUsages;

public class VRWCPPPauseMenu : MonoBehaviour
{

    // public static bool paused = false;
    //public GameObject PauseMenuCanvas;

    private static UnityEngine.XR.InputDevice targetDevice;



    // Start is called before the first frame update
    void Start()
    {

        //var inputDevices = new List<UnityEngine.XR.InputDevice>();
        //UnityEngine.XR.InputDevices.GetDevices(inputDevices);

        //foreach (var device in inputDevices)
        //{
        //    Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.characteristics.ToString()));
        //}
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        targetDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        if (targetDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool isPressed))
        {
            Debug.Log("OOGA BOGGA ");
        }

        Debug.Log(isPressed);

        //InputSystem; Had to comment out for project to work

        //Debug.Log(Input.GetJoystickNames() + "Show yourself");
    }
}
