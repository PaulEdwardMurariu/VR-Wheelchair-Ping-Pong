using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBoundsHandler : MonoBehaviour
{
    private GameObject scoreboard;
    private Positiondetection positionDetection;
    private ballcollision ballCollision;

    void Start()
    {
        scoreboard = GameObject.FindWithTag("Scoreboard");
        positionDetection = GetComponent<Positiondetection>();
        ballCollision = GetComponent<ballcollision>();
        
        // Verify that the ball has this component
        Debug.Log("OutOfBoundsHandler attached to: " + gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
        // Log the trigger collision
        Debug.Log("Ball entered trigger: " + other.name);
        
        // Check if we hit any of the out-of-bounds colliders
        if (other.name.StartsWith("OutOfBounds") || other.name.StartsWith("OutofBounds"))
        {
            Debug.Log("Ball is out of bounds at: " + other.name);
            
            if (ballCollision.turretball)
            {
                Destroy(gameObject);
                return;
            }

            bool isAIServe = positionDetection.teamserve;
            bool AIHitLast = positionDetection.whohitlast;

            // Handle scoring based on which out-of-bounds area was hit
            if (other.name == "OutOfBounds_RightSide" || other.name == "OutOfBounds_BehindAI" ||
                other.name == "OutofBounds_BehindAI")
            {
                // Ball went out on AI's side
                if (AIHitLast)
                {
                    // AI hit it out, point for player
                    scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(true);
                }
                else
                {
                    // Player hit it out on AI's side, point for AI
                    scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(false);
                }
            }
            else if (other.name == "OutOfBounds_LeftSide" || other.name == "OutOfBounds_BehindPlayer" ||
                     other.name == "OutofBounds_BehindPlayer")
            {
                // Ball went out on player's side
                if (AIHitLast)
                {
                    // AI hit it out on player's side, point for player
                    scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(true);
                }
                else
                {
                    // Player hit it out on their side, point for AI
                    scoreboard.GetComponent<ScoreboardScript>().IncreaseScore(false);
                }
            }

            // Reload the scene to reset for next point
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
} 