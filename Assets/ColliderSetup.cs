using UnityEngine;

public class ColliderSetup : MonoBehaviour
{
    void Start()
    {
        // Find all objects with names starting with "OutOfBounds" or "OutofBounds"
        GameObject[] outOfBoundsObjects = GameObject.FindObjectsOfType<GameObject>();
        
        foreach (GameObject obj in outOfBoundsObjects)
        {
            if (obj.name.StartsWith("OutOfBounds") || obj.name.StartsWith("OutofBounds"))
            {
                BoxCollider boxCollider = obj.GetComponent<BoxCollider>();
                
                if (boxCollider != null)
                {
                    // Ensure the collider is set as a trigger
                    if (!boxCollider.isTrigger)
                    {
                        Debug.Log("Setting collider " + obj.name + " to be a trigger");
                        boxCollider.isTrigger = true;
                    }
                    else
                    {
                        Debug.Log("Collider " + obj.name + " is already a trigger");
                    }
                }
                else
                {
                    Debug.LogWarning("OutOfBounds object " + obj.name + " does not have a BoxCollider!");
                }
            }
        }
    }
} 