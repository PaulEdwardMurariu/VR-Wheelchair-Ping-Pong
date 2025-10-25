using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTutorialPosition : MonoBehaviour
{

    private Vector3 playerPosition;
    private Vector3 XROriginPosition;
    public Transform xrorigin;


    // Start is called before the first frame update
    void Start()
    {
        playerPosition = new Vector3(-3.05999994f, 0.100000001f, 0.140000001f);
        transform.position = playerPosition;
        XROriginPosition = new Vector3(-3.05999994f, 1.5f, 0.140000001f);
        xrorigin.position = XROriginPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
