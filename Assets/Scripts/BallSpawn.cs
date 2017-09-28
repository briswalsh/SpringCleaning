using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour {

    public GameObject movable;

    public GameObject ball;
    public Vector3 origin;

    private GameObject currBall;

    /* Game States */
    public GameObject[] spotlights;
    public GameObject[] walls;

    private int state;
    public int ballCount;
    private int maxCount;

    //private Cremetoria fire;

    /* Gravity */
    public bool alt;
    public bool gravOn;
    public float gravConst;
    public float duration;

    private float t;
    private AudioSource hum;

    /* Vacuum */
    public bool[] vacuumOn;
    public GameObject[] vacuumObj;

    void Awake()
    {
        vacuumObj = GameObject.FindGameObjectsWithTag("Vacuum");
        vacuumOn = new bool[vacuumObj.Length];
        for (int i = 0; i < vacuumObj.Length; i++)
        {
            vacuumOn[i] = false;
            vacuumObj[i].GetComponent<VacuumPhysics>().vacuumSwitch = false;
        }

        walls = GameObject.FindGameObjectsWithTag("Wall");
        TurnOnWalls();
    }

    // Use this for initialization
    void Start () {
        movable = GameObject.FindGameObjectWithTag("Movable");
        currBall = Instantiate(ball, origin, new Quaternion(), movable.transform);

        /* Initial Game State */
        state = 0;
        maxCount = ballCount;

        for (int i = 0; i < spotlights.Length; i++)
        {
            spotlights[i].SetActive(false);
        }
        spotlights[state].SetActive(true);

//        fire = GetComponent<Cremetoria>();

        /* Gravity */
        SetGravity(true);
        t = Time.time;
        hum = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
		if (currBall == null)
        {
            currBall = Instantiate(ball, origin, new Quaternion(), movable.transform);
            TurnOnWalls();
        }

        if (alt)
        {
            if(Time.time > t + duration)
            {
                gravOn = !gravOn;
                SetGravity(gravOn);
                t = Time.time + duration;
            }
        }
    }

    void Decrement()
    {
        ballCount--;
        if(ballCount <= 0)
        {
            //fire.Immolate();
        }
    }

    public void NextStage()
    {
        spotlights[state].SetActive(false);
        state++;
        spotlights[state].SetActive(true);
        if(state == 1)
        {
            alt = true;
            t = Time.time;
        }
        if(state == 2)
        {
            alt = false;
            SetGravity(true);
        }
        if(state == 3)
        {
            //win
        }
    }

    void SetGravity(bool on)
    {
        for (int i = 0; i < movable.transform.childCount; i++)
        {
            var sphere = movable.transform.GetChild(i).GetComponent<IPhysics>();
            if (sphere != null)
            {
                if (on)
                {
                    sphere.GravityControl(on, gravConst);
                }
                else
                {
                    sphere.GravityControl(on, -0.1f);
                }
            }
        }
    }

    void TurnOnWalls()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].GetComponent<Collider>().enabled = true;
        }
    }
}