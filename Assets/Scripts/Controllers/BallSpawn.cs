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
    public GameObject[] wicketOrder;

    private int state;
    public int ballCount;
    private int maxCount;

    public Crematoria fire;

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

    /* Sounds */
    public GameObject soundManager;
	private SoundsController sfx;

    /* Ground */
    public GameObject roomFloor;

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

        for (int i = 0; i < wicketOrder.Length; i++)
        {
            wicketOrder[i].SetActive(false);
        }
    }

    // Use this for initialization
    void Start () {
        movable = GameObject.FindGameObjectWithTag("Movable");
        currBall = Instantiate(ball, origin, new Quaternion(), movable.transform);
        try
        {
            sfx = soundManager.GetComponent<SoundsController>();
        }
        catch
        {
            print("Could not load sounds controller: is sound manager set to an instance in the ball spawn script?");
        }

        /* Initial Game State */
        state = 0;
        maxCount = ballCount;

        for (int i = 0; i < spotlights.Length; i++)
        {
            spotlights[i].SetActive(false);
        }
        spotlights[state].SetActive(true);
        wicketOrder[state].SetActive(true);

        /* Gravity */
        SetGravity(true);
        t = Time.time;
    }

    // Update is called once per frame
    void Update () {
		if (currBall == null)
        {
            currBall = Instantiate(ball, origin, new Quaternion(), movable.transform);
            TurnOnWalls();
            SetGravity(gravOn);
        }

        if (alt)
        {
            if(Time.time >= t + duration)
            {
                gravOn = !gravOn;
                if (gravOn)
                {
					sfx.PlaySound ("grav-up");
                }
                else
                {
					sfx.PlaySound ("grav-down");
                }

                SetGravity(gravOn);
                t = Time.time + duration;
            }
        }
    }

    public void Decrement()
    {
        ballCount--;
        if(ballCount <= 0)
        {
            fire.Immolation();
        }
    }

    public bool NextStage()
    {
        wicketOrder[state].SetActive(false);
        spotlights[state].SetActive(false);
        state++;
        if(state != 3)
        {
			sfx.PlayDirectionalSound("wicket-ding",wicketOrder[state].transform.position);
        }
        if(state == 1)
        {
            alt = true;
            t = Time.time;
            spotlights[state].SetActive(true);
            wicketOrder[state].SetActive(true);
        }
        if (state == 2)
        {
			sfx.PlayDirectionalLoop ("vacuum",wicketOrder[2].transform.position);
            alt = false;
            SetGravity(true);
            for(int i = 0; i < vacuumObj.Length; i++)
            {
                if(vacuumObj[i].transform.parent.name == "Wicket 8A")
                {
                    vacuumOn[i] = true;
                }
            }
            spotlights[state].SetActive(true);
            wicketOrder[state].SetActive(true);
        }
        if (state == 3)
        {
			sfx.Win ();
            for (int i = 0; i < vacuumObj.Length; i++)
            {
                vacuumOn[i] = false;
            }
            //win
            Destroy(roomFloor);
        }
        return true;
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
                    print(sphere);
                    sphere.GravityControl(on, -0.05f);
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
