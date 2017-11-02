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
    public GravityControl gc;

    /* Vacuum */
    public bool[] vacuumOn;
    public GameObject[] vacuumObj;

    /* Sounds */
    public GameObject soundManager;
	private SoundsController sfx;
	bool fail1played;
	bool failGravPlayed;
	bool failVacPlayed;

    /* Ground */
    public GameObject roomFloor;
    private bool win;

    /* Particles */
    public GameObject gravParticles;
    public GameObject vacuumParticles;

    /* Sphere Color Coding */
    public Material[] sphereColor;
    public Color[] lightColor;

    void Awake()
    {
        gc = GetComponent<GravityControl>();
        vacuumObj = GameObject.FindGameObjectsWithTag("Vacuum");
		fail1played = false;
		failGravPlayed = false;
		failVacPlayed = false;
        vacuumOn = new bool[vacuumObj.Length];
        for (int i = 0; i < vacuumObj.Length; i++)
        {
            vacuumOn[i] = false;
            vacuumObj[i].GetComponent<VacuumPhysics>().vacuumSwitch = false;
        }

        walls = GameObject.FindGameObjectsWithTag("Wall");
       // TurnOnWalls();

        for (int i = 0; i < wicketOrder.Length; i++)
        {
            wicketOrder[i].SetActive(false);
        }
    }

    // Use this for initialization
    void Start () {
        movable = GameObject.FindGameObjectWithTag("Movable");
        currBall = Instantiate(ball, origin, new Quaternion(), movable.transform);
        currBall.GetComponent<Renderer>().material = sphereColor[state];
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
        vacuumParticles.SetActive(false);

        for (int i = 0; i < spotlights.Length; i++)
        {
            spotlights[i].SetActive(false);
        }
        spotlights[state].SetActive(true);
        wicketOrder[state].SetActive(true);

        win = false;

        for (int i = 0; i < vacuumObj.Length; i++)
        {
            if (vacuumObj[i].transform.parent.name == "Wicket 1A")
            {
                vacuumOn[i] = true;
            }
        }
        sfx.Narrate("intro1");
    }

    // Update is called once per frame
    void Update () {
        //print("I'm updating");
        if (currBall == null && win == false && ballCount > 0)
        {
            SpawnBall();
        }
        //print("I finished updating");
    }

    public void SpawnBall()
    {
	sfx.PlaySound ("pneumatic");
        currBall = Instantiate(ball, origin, new Quaternion(), movable.transform);
        currBall.GetComponent<Renderer>().material = sphereColor[state];
        currBall.GetComponentInChildren<Light>().color = lightColor[state];
        //currBall.GetComponent<Animation>().Play();
        TurnOnWalls();
    }

    public IEnumerator SpawnBall(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnBall();
    }
    
    public void Decrement()
    {
        ballCount--;
        if(ballCount == 0)
        {
            gc.DisableAltGrav();
            spotlights[state].SetActive(false);
            sfx.Cut ();
			sfx.Narrate ("loseGame");
            vacuumParticles.SetActive(false);
            StartCoroutine(PainfulDeath());
        } else {
			if (!(sfx.IsNarrating ())){
				if (!(fail1played)) {
					sfx.Narrate ("firstFail");
					fail1played = true;
				} else if (state == 1) {
					if ( !failGravPlayed) {
						sfx.Narrate ("gravFail");
						failGravPlayed = true;
					}
				} else if (state == 2) {
					if (!failVacPlayed) {
						sfx.Narrate ("suckFail");
						failVacPlayed = true;
					}
				}
			}
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
            gc.EnableAltGrav();
            spotlights[state].SetActive(true);
            wicketOrder[state].SetActive(true);
            for (int i = 0; i < vacuumObj.Length; i++)
            {
                if (vacuumObj[i].transform.parent.name == "Wicket 2B")
                {
                    vacuumOn[i] = true;
                }
            }
			sfx.Narrate ("gravOn");
        }
        if (state == 2)
        {
            gc.DisableAltGrav();
			sfx.PlayDirectionalLoop ("vacuum",wicketOrder[2].transform.position);
            for(int i = 0; i < vacuumObj.Length; i++)
            {
                if(vacuumObj[i].transform.parent.name == "Wicket 3A")
                {
                    vacuumOn[i] = true;
                }
            }
            spotlights[state].SetActive(true);
            wicketOrder[state].SetActive(true);
            gravParticles.SetActive(false);
            vacuumParticles.SetActive(true);
            sfx.Narrate ("suckOn");
        }
        if (state == 3)
        {
            vacuumParticles.SetActive(false);
            sfx.Cut ();
			sfx.Win ();
            for (int i = 0; i < vacuumObj.Length; i++)
            {
                vacuumOn[i] = false;
            }
            //win
            win = true;
            StartCoroutine(DestroyRoomFloor());
			sfx.Narrate ("winGame");
        }
        return true;
    }

    IEnumerator DestroyRoomFloor()
    {
        yield return new WaitForSeconds(14 + 8.7f);
        Destroy(roomFloor);
    }

    IEnumerator PainfulDeath()
    {
        yield return new WaitForSeconds(10);
        sfx.StopSoundLoop();
        sfx.PlaySound("torch");
        fire.Immolation();
    }

    void TurnOnWalls()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].GetComponent<Collider>().enabled = true;
        }
    }
    
}
