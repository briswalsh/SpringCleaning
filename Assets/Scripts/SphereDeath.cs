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
    public AudioSource bloodCurdling;

    // Use this for initialization
    void Start()
    {
        deathRow = false;
        smores = false;
        sphereDude = GetComponent<MeshRenderer>();
        bloodCurdling = GetComponent<AudioSource>();
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
            //Destroy(this.gameObject);
            if (smores == false)
            {
                Vector3 firePosn = transform.position;
                //firePosn.x += -10.3f;
                //firePosn.z += 20f;
                smores = true;
                sphereDude.enabled = false;
                GameObject myFire = Instantiate(fire);
                myFire.transform.position = firePosn;
                bloodCurdling.Play();
            }
        }

        if (lifespan <= -1*deathspan)
        {
            Destroy(this.gameObject);
        }

        //Debug.Log(lifespan + " " + deathRow);
    }


    /* Collision Detectors */
    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Safe Area")
        {
            print("My time is nigh");
            deathRow = true;
        }
    }
}
