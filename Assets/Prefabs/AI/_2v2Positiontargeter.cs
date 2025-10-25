using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class _2v2Positiontargeter : MonoBehaviour
{
    public bool hitAIrecently = false;
    private float tempdistancez;
    private float tempdistancex;
    public float distancefromradius = 0.5f;
    private bool hittheballrecently;
    //ball to spawn for serving 
    public GameObject serveball;
    
    public NavMeshPath path;
    public bool playerteam = false;
    public bool isserving = false;
    public float Xlimit = 1.5f;
    // teannates targe and oponents 
    public GameObject target;
    public GameObject opponent1;
    public GameObject opponent2;
    public GameObject Teammate;
   
    private NavMeshAgent agent;
    private Transform temp;
    private Vector3 tempposition;
    public int randomvalue;
    public bool rolled = false;
    private Transform temptransform;
    public bool servinghit = false;
    // position values 
    private float x;
    private float y;
    private float z;
    public Vector3 center;
    public bool isxlimig1 = true;
    public float xmax;
    public bool oponent1player;
    public bool opponent2player;
    public int randomnum;
    public bool readytostart = false;
    public Vector3 j;
    private bool waittomove = true;
    public float zmin;
    public float zmax;
    private Vector3 ballsendpoint;
    private bool waittemp =true;
    public int scoreboardnum;
    public GameObject scoreboard;
    private ScoreboardScript SBScript;
    private bool server;
    public bool innovolley = false;

    // Movement and positioning
    private Vector3 defaultPosition;
    private float moveSpeed = 3.0f;
    private float returnSpeed = 2.0f;
    private bool isReturning = false;

    private void Awake()
    {
        scoreboard = GameObject.FindWithTag("Scoreboard");
        SBScript = scoreboard.GetComponent<ScoreboardScript>();
        
        // Initialize serving state and handle AI destruction
        if (SBScript.getserve() == scoreboardnum)
        {
            isserving = true;
            if (playerteam)
            {
                destroynonserverinvert();
            }
        }
        else if (SBScript.getserve() == 0)
        {
            if (playerteam)
            {
                destroynonserver();
            }
        }
        else if (!isserving && gameObject.name == "AI2v2 t2")
        {
            Destroy(gameObject);
        }

        // Set default position based on team side
        defaultPosition = transform.position;
    }

    void Start()
    {
        if (gameObject.name == "AI2v2 t2")
        {
            opponent1.gameObject.GetComponent<_2v2Positiontargeter>().opponent1 = this.gameObject;
            opponent2.gameObject.GetComponent<_2v2Positiontargeter>().opponent1 = this.gameObject;
        }

        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        if (Xlimit > 0) {
            isxlimig1 = true;
        }
        else {
            isxlimig1 = false;
        }

        if (!agent.isOnNavMesh)
        {
            Debug.LogWarning("AI is not on NavMesh!");
            return;
        }
        
        agent.speed = moveSpeed;
        agent.angularSpeed = 120;
        agent.acceleration = 8;
        agent.stoppingDistance = 0.1f;

        StartCoroutine(waittostart());
    }

    private void destroynonserver()
    {
        if (playerteam)
        {
            if (gameObject.name == "AI2v2 t2")
            {
                if (SBScript.getplayerScore() % 2 == 0)
                {
                    Destroy(gameObject);
                }
                else
                {


                }

            }
            else
            {
                if (SBScript.getplayerScore() % 2 == 0)
                {

                }
                else
                {
                    Destroy(gameObject);

                }

            }
        }
        return;

    }
    private void destroynonserverinvert() {
        if (playerteam)
        {
            if (gameObject.name == "AI2v2 t2")
            {
                if (SBScript.getplayerScore() % 2 == 0)
                {
                    
                }
                else
                {
                    Destroy(gameObject);

                }

            }
            else
            {
                if (SBScript.getplayerScore() % 2 == 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    

                }

            }
        }
        return;

    }
    // Update is called once per frame
    void Update()
    {
        if (!readytostart) return;

        if (isserving && !servinghit)
        {
            servetheball();
            return;
        }

        if (target != null)
        {
            MoveTowardsBall();
        }
        else
        {
            ReturnToDefaultPosition();
        }
    }

    private void MoveTowardsBall()
    {
        if (!agent.isOnNavMesh) return;

        Vector3 ballPosition = target.transform.position;
        Vector3 ballVelocity = target.GetComponent<Rigidbody>().velocity;
        
        // Predict where the ball will be
        Vector3 predictedPosition = ballPosition + ballVelocity * 0.5f;
        
        // Calculate target position for the AI
        Vector3 targetPosition = transform.position;
        
        // If ball is coming to our side
        if ((playerteam && predictedPosition.x < 0) || (!playerteam && predictedPosition.x > 0))
        {
            // Move to intercept
            targetPosition = new Vector3(
                Mathf.Clamp(predictedPosition.x, -Xlimit, Xlimit),
                transform.position.y,
                Mathf.Clamp(predictedPosition.z, -1.5f, 1.5f)
            );
            
            agent.speed = moveSpeed;
            isReturning = false;
        }
        else if (!isReturning)
        {
            // Return to default position when ball is on other side
            ReturnToDefaultPosition();
            isReturning = true;
        }

        agent.SetDestination(targetPosition);
    }

    private void ReturnToDefaultPosition()
    {
        if (!agent.isOnNavMesh) return;
        
        agent.speed = returnSpeed;
        agent.SetDestination(defaultPosition);
    }

    private void servetheball()
    {
        j = transform.position;
        j.y = 1.5f;
        
        if (playerteam)
        {
            j.x += 0.2f;
        }
        else
        {
            j.x -= 0.2f;
        }
        
        target = Instantiate(serveball, j, Quaternion.identity);
        target.GetComponent<Positiondetection>().setplaying();
        isserving = false;
        servinghit = true;

        if (Teammate != null && Teammate.CompareTag("AI"))
        {
            Teammate.GetComponent<_2v2Positiontargeter>().target = target;
        }
    }

    private void targetball() {
        if (!target)
            return;

        ballsendpoint = target.GetComponent<BallPredictor>().endPoint; // dont have to keep getting this 
        if (isxlimig1)// checks which side the AI is on 
        {
            if (!target.GetComponent<ballcollision>().bouncedopponent) {
                return;
                
            }
            else {
                servinghit = true;
                StartCoroutine(waittempstart());

            }
            if (waittemp) {
                return;
            }

            if (!target.GetComponent<ballcollision>().bouncedplayer)
            {
                servinghit = true;
            }
            else
            {
                servinghit = false;
                StartCoroutine(waittempstart());
            }
              if (waittemp) {
                return;
            }
            // on the positve x axis 
            if (ballsendpoint.x > Xlimit)
            {
                if (ballsendpoint.z > zmin && ballsendpoint.z < zmax) //if (Mathf.Abs(distance(ballsendpoint)) > distancefromradius) // makes sure that the endpoint is far enough from the other player
                {
                    agent.SetDestination(ballsendpoint);// sets destincation  to the balls endpoint
                    if (!(agent.CalculatePath(ballsendpoint, path)))// error prevention 
                    {
                        agent.SetDestination(target.gameObject.transform.position);

                    }
                  


                }
                else
                {
                    agent.SetDestination(center); // sets to center if this fails 


                }
            }
            else {


                agent.SetDestination(center);// sets to center if ball is on the other side of the field 

            }

        
          
        }

        else {
            if (!target.GetComponent<ballcollision>().bouncedplayer)
            {
                return;
            }
            else
            {
                servinghit = true;
                StartCoroutine(waittempstart());

            }
            if (waittemp)
            {
                return;
            }
            if (!target.GetComponent<ballcollision>().bouncedopponent)
            {
                servinghit = true;
            }
            else {
                servinghit = false;
            }
            // on the negative x axis 
            if (ballsendpoint.x < Xlimit)
            {
// Debug.Log("we Made it here");
                if (ballsendpoint.z > zmin && ballsendpoint.z < zmax)//if (Mathf.Abs(distance(ballsendpoint)) > distancefromradius  )
                {
                  

                    agent.SetDestination(ballsendpoint);// sets destincation  to the balls endpoint
                    if (!(agent.CalculatePath(ballsendpoint, path)))// error prevention 
                    {
                        agent.SetDestination(target.gameObject.transform.position );

                    }
                }
                else {
                    agent.SetDestination(center);// sets to center if this fails 


                }
            }
            else
            {


                agent.SetDestination(center);// sets to center if ball is on the other side of the field 

            }




        }


    }
    private float distance(Vector3 wayfrom) {
        tempdistancex = wayfrom.x - Teammate.transform.position.x;
        tempdistancez = wayfrom.z - Teammate.transform.position.z;
        tempdistancex *= tempdistancex;
        tempdistancez *= tempdistancez;
        return Mathf.Sqrt(tempdistancex + tempdistancez);
    }
    /*
    private void xyznottoobigorsmall()
    {
        if (x > xlimit)
            x = xlimit;
        if (x < -xlimit)
            x = -xlimit;
        if (z > zlimit)
            z = zlimit;
        if (z < -zlimit)
            z = -zlimit;
        if (y < -ylimit)
            y = -ylimit;
        if (y > ylimit)
            y = ylimit;

    }*/
    IEnumerator ballhitDelayRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        hitAIrecently = false;
    }
    IEnumerator waittostart()
    {
        yield return new WaitForSeconds(2);
        readytostart = true;
    }
    IEnumerator waittempstart()
    {
        yield return new WaitForSeconds(0.2f);
        waittemp = false;
        StartCoroutine(waittoxmove());
    }
    IEnumerator waittoxmove()
    {
        yield return new WaitForSeconds(0.5f);
        waittomove = false;
    }
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Ball") && !hitAIrecently)
        {
            Rigidbody ballRb = coll.gameObject.GetComponent<Rigidbody>();
            if (ballRb != null)
            {
                // Reset velocity
                ballRb.velocity = Vector3.zero;
                
                // Calculate return direction
                Vector3 returnDirection;
                if (playerteam)
                {
                    returnDirection = new Vector3(1f, 0.8f, Random.Range(-0.5f, 0.5f));
                }
                else
                {
                    returnDirection = new Vector3(-1f, 0.8f, Random.Range(-0.5f, 0.5f));
                }
                
                // Apply force
                ballRb.AddForce(returnDirection.normalized * 12f, ForceMode.Impulse);
                
                // Update ball state
                coll.gameObject.GetComponent<Positiondetection>().AIhistlast();
                coll.gameObject.GetComponent<Positiondetection>().setbouncedfalse();
            }

            hitAIrecently = true;
            StartCoroutine(ballhitDelayRoutine());
        }
    }
}
