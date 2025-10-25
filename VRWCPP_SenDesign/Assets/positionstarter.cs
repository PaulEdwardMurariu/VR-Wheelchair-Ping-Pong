using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionstarter : MonoBehaviour
{
    public bool turretmode = true;
    private Vector3 altposition;
    private Vector3 xrposition;
    public bool rightsidestart;
    private GameObject scoreboard;
    private bool servesequence = true;// we are serving if true 
    private bool teamservesequence = true;// teamate is serving if true 
    private bool teamserve = true;
    public bool isdoubles = false;
    public Transform xrorigin;


    // Start is called before the first frame update
    void Start()
    {


        if (turretmode)//turret mode does not care about any of this its more of a fun scene 
        {


        }
        else
        {
            scoreboard = GameObject.FindWithTag("Scoreboard");
            //gets weather or not the player is serving 
            int servenum = scoreboard.GetComponent<ScoreboardScript>().getserve();
            if (servenum == 0)
            {
                servesequence = true;
                teamservesequence = false;
            }
            else if (servenum == 1)
            {//checking to see if our teammate is serving 
                teamservesequence = true;
                servesequence = false;
            }
            else
            {
                servesequence = false;
                teamservesequence = false;
            }



            if (servesequence)
            {
                weserve();
            }
            else if (teamservesequence)
            {
                teamserveing();


            }
            else if (!isdoubles)
            {

                AIserve();
            }
            else
            {
                altposition = new Vector3(-4.5f, 0.12f, 0f);
                transform.position = altposition;
                xrposition = new Vector3(-4.5f, 0.1f, 0f);
                xrorigin.position = xrposition;
            }

        }



    }

    void weserve()
    {
        if ((scoreboard.GetComponent<ScoreboardScript>().getplayerScore() % 2) == 0)
        {
            rightsidestart = true;
        }
        else
        {
            rightsidestart = false;
        }
        if (rightsidestart)
        {

            altposition = new Vector3(-2.9f, 0.12f, 0f);
            transform.position = altposition;
            xrposition = new Vector3(-2.9f, 1.5f, 0f);
            xrorigin.position = xrposition;
        }
        else
        {

            altposition = new Vector3(-2.9f, 0.12f, 0f);
            transform.position = altposition;
            xrposition = new Vector3(-2.9f, 1.5f, 0f);
            xrorigin.position = xrposition;
        }

    }
    void teamserveing()
    {
        if ((scoreboard.GetComponent<ScoreboardScript>().getplayerScore() % 2) == 0)
        {
            rightsidestart = true;
        }
        else
        {
            rightsidestart = false;
        }
        if (!rightsidestart)
        {

            altposition = new Vector3(-4.5f, 0.12f, 0f);
            transform.position = altposition;
        }
        else
        {

            altposition = new Vector3(-4.5f, 0.12f, 0f);
            transform.position = altposition;
            xrposition = new Vector3(-2.9f, 1.5f, 0f);
            xrorigin.position = xrposition;
        }

    }

    void AIserve()
    {
        if ((scoreboard.GetComponent<ScoreboardScript>().getaiScore() % 2) == 0)
        {
            rightsidestart = true;
        }
        else
        {
            rightsidestart = false;
        }
        if (rightsidestart)
        {

            altposition = new Vector3(-2.9f, 0.12f, 0f);
            transform.position = altposition;
            xrposition = new Vector3(-2.9f, 1.5f, 0f);
            xrorigin.position = xrposition;
        }
        else
        {

            altposition = new Vector3(-2.9f, 0.12f, 0f);
            transform.position = altposition;
            xrposition = new Vector3(-2.9f, 1.5f, 0f);
            xrorigin.position = xrposition;
        }
        
    }


    // Update is called once per frame
    void Update()
    {

    }
}
