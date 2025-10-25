using UnityEngine;

public class Set2v2Position : MonoBehaviour
{
    private Vector3 playerPosition;
    private Vector3 XROriginPosition;
    public Transform xrorigin;

    void Start()
    {
        // Set player position at wheelchair height
        playerPosition = new Vector3(transform.position.x, 0.1f, transform.position.z);
        transform.position = playerPosition;

        // Set XR Origin slightly above wheelchair for proper VR view
        XROriginPosition = new Vector3(transform.position.x, 0.4f, transform.position.z);
        xrorigin.position = XROriginPosition;
    }
} 