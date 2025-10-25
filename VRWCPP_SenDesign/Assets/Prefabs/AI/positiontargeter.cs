using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class positiontargeter : MonoBehaviour
{
    public GameObject scoreboard;
    public bool hitAIrecently = false;
    private bool hittheballrecently;
    public GameObject serveball;
    public bool isserving = false;
    public bool servinghit = false;
    public float Xlimit = 1.5f;
    public GameObject target;
    public GameObject playerframe;
    public GameObject player;
    private NavMeshAgent wheelchairAgent;
    private Transform temp;
    private Vector3 tempposition;
    public int randomvalue;
    public bool rolled = false;
    private Transform temptransform;
    public float x;
    public float y;
    public float z;
    public Vector3 addforce;
    public Vector3 j;
    public bool readytostart = false;
    public bool shorthit;
    public bool rightsidestart;
    private Vector3 altpotiion;
    public bool innovolley = false;

    private Vector3[] trajectories;
    private int volleyCount = 0;
    private int nextTrajectoryIndex = 0;
    public bool useRandomTrajectories = true;

    void Start()
    {
        scoreboard = GameObject.FindWithTag("Scoreboard");

        trajectories = new Vector3[]
        {
            new Vector3(-1.5f, 0.8f, 0f),
            new Vector3(-1.2f, 1.2f, 0f),
            new Vector3(-0.9f, 0.7f, 0f),
            new Vector3(-1.0f, 1.0f, 0f),
            new Vector3(-0.8f, 0.4f, 0f),
            new Vector3(-1.3f, 0.9f, 0f)
        };

        wheelchairAgent = GetComponent<NavMeshAgent>();
        wheelchairAgent.updateRotation = false;

        if (scoreboard.GetComponent<ScoreboardScript>().getserve() == 2)
            isserving = true;
        else
        {
            isserving = false;
            shorthit = true;
        }

        findstartposition();
        setstartposition();
    }

    private Vector3 SelectTrajectory()
    {
        if (useRandomTrajectories)
        {
            if (Random.value < 0.8f)
            {
                int index = Random.Range(0, 4);
                if (index == 4) index = 5;
                return trajectories[index];
            }
            else return trajectories[4];
        }
        else
        {
            Vector3 trajectory = trajectories[nextTrajectoryIndex];
            nextTrajectoryIndex = (nextTrajectoryIndex + 1) % trajectories.Length;
            return trajectory;
        }
    }

    private void findstartposition()
    {
        if (isserving)
        {
            rightsidestart = (scoreboard.GetComponent<ScoreboardScript>().getaiScore() % 2) == 0;
        }
        else
        {
            rightsidestart = (scoreboard.GetComponent<ScoreboardScript>().getplayerScore() % 2) == 0;
        }
    }

    private void setstartposition()
    {
        altpotiion = new Vector3(1.55f, 0.04f, -0.09f);
        transform.position = altpotiion;
    }

    void Update()
    {
        if (!readytostart)
        {
            StartCoroutine(waittostart());
        }

        if (isserving && readytostart)
        {
            servetheball();
        }

        move();

        if (player != null)
        {
            Vector3 lookDirection = player.transform.position - transform.position;
            lookDirection.y = 0f;
            if (lookDirection != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = lookRotation;
            }
        }
    }

    IEnumerator waittostart()
    {
        yield return new WaitForSeconds(3);
        readytostart = true;
    }

    IEnumerator ballhitDelayRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        hitAIrecently = false;
    }

    private void servetheball()
    {
        j = transform.position;
        j.y = 1.5f;
        target = Instantiate(serveball, j, Quaternion.identity);

        if (!target.GetComponent<OutOfBoundsHandler>())
            target.AddComponent<OutOfBoundsHandler>();

        target.GetComponent<Positiondetection>().setplaying();
        isserving = false;
        servinghit = true;
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Ball" && !hitAIrecently)
        {
            coll.gameObject.GetComponent<Positiondetection>().setbouncedfalse();
            coll.gameObject.GetComponent<Positiondetection>().AIhistlast();

            if (!coll.gameObject.GetComponent<ballcollision>().bouncedopponent)
            {
                Vector3 selectedTrajectory = SelectTrajectory();
                coll.rigidbody.velocity = Vector3.zero;
                coll.rigidbody.AddForce(selectedTrajectory, ForceMode.Impulse);
                coll.gameObject.GetComponent<ballcollision>().bounces = 0;
                coll.gameObject.GetComponent<Positiondetection>().bounced = false;
            }

            hitAIrecently = true;
            StartCoroutine(ballhitDelayRoutine());
        }
    }

    void move()
    {
        if (!target) return;

        // Try to get BallPredictor component
        BallPredictor ballPredictor = target.GetComponent<BallPredictor>();
        if (!ballPredictor) return;

        Vector3 targetPosition = ballPredictor.endPoint;
        
        // If the ball is coming to AI's side
        if (targetPosition.x > 0)
        {
            // Calculate target position for wheelchair, keeping current Y and Z
            Vector3 wheelchairTarget = transform.position;
            wheelchairTarget.x = Mathf.Clamp(targetPosition.x, 0, Xlimit);
            wheelchairTarget.z = Mathf.Clamp(targetPosition.z, -1.5f, 1.5f); // Allow some side-to-side movement

            wheelchairAgent.SetDestination(wheelchairTarget);
            wheelchairAgent.speed = 3f; // Increased speed for better response
            rolled = false;
        }
        // If the ball is on player's side, return to ready position
        else if (targetPosition.x < 0)
        {
            if (!rolled)
            {
                randomvalue = Random.Range(1, 3);
                rolled = true;
            }

            // Return to a position mirroring the player's position
            Vector3 returnPosition = transform.position;
            returnPosition.x = 1.0f; // Default X position on AI side
            if (randomvalue == 1)
                returnPosition.z = 0;
            else
                returnPosition.z = -targetPosition.z * 0.5f;

            wheelchairAgent.SetDestination(returnPosition);
        }
    }
}

