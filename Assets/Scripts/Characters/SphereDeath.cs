using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDeath : MonoBehaviour
{
    [Header("Deathiness")]
    public bool deathRow;
    public float lifespan = 5f;
    public float deathspan = 2f;
    public GameObject fire;
    private bool smores;
    private MeshRenderer sphereDude;
    private Vector3 firePosn;

    public GameObject soundManager;
	private SoundsController sfx;

    private GameObject movable;
    
	public int state;

    // Use this for initialization
    void Start()
    {
        deathRow = false;
        smores = false;
        sphereDude = GetComponent<MeshRenderer>();
        movable = GameObject.FindGameObjectWithTag("Movable");
		soundManager = GameObject.Find ("SoundManager");
        sfx = soundManager.GetComponent<SoundsController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deathRow)
        {
            lifespan -= Time.deltaTime;
        }

        if (lifespan <= 0)
        {
            if (smores == false)
            {
                firePosn = transform.position;
                smores = true;
                sphereDude.enabled = false;
                GameObject myFire = Instantiate(fire);
                myFire.transform.position = firePosn;
				if (state == 2) {
					sfx.PlayDirectionalSound ("torch-scream", transform.position);
				} else {
					sfx.PlayDirectionalSound ("torch", transform.position);
				}

                StartCoroutine(movable.GetComponent<BallSpawn>().SpawnBall(0.5f));
            }
            else
            {
                sphereDude.transform.position = firePosn;
            }
        }

        if (lifespan <= -1*deathspan)
        {
            movable.GetComponent<BallSpawn>().Decrement();
            Destroy(this.gameObject);
        }
    }


    /* Collision Detectors */

    private void OnTriggerEnter(Collider other)
    {
        var wc = other.gameObject.GetComponent<WicketCollision>();
        if(wc != null)
        {
            deathRow = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Safe Area")
        {
            print("My time is nigh");
            deathRow = true;
        }
    }
}
