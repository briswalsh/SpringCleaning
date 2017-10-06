using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour {

    public bool gravOn;
    public float grav;
    public float noGrav;

    public bool alt;
    public float duration;
    private float t;

    public GameObject movable;

    /* Sounds */
    public GameObject soundManager;
    private SoundsController sfx;


    // Use this for initialization
    void Start () {
        SetGravity(true);
        t = Time.time;

        movable = GameObject.FindGameObjectWithTag("Movable");

        try
        {
            sfx = soundManager.GetComponent<SoundsController>();
        }
        catch
        {
            print("Could not load sounds controller: is sound manager set to an instance in the ball spawn script?");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (alt)
        {
            if (Time.time >= t + duration)
            {
                gravOn = !gravOn;
                if (gravOn)
                {
                    sfx.PlaySound("grav-up");
                }
                else
                {
                    sfx.PlaySound("grav-down");
                }

                SetGravity(gravOn);
                t = Time.time + duration;
            }
        }
    }

    public void EnableAltGrav()
    {
        alt = true;
        t = Time.time;
    }

    public void DisableAltGrav()
    {
        alt = false;
        gravOn = true;
        SetGravity(true);
    }

    private void SetGravity(bool on)
    {
        Physics.gravity = new Vector3(0, on ? grav : noGrav, 0);
        /*
        for (int i = 0; i < movable.transform.childCount; i++)
        {
            var sphere = movable.transform.GetChild(i).GetComponent<IPhysics>();
            if (sphere != null)
            {
                if (on)
                {
                    sphere.GravityControl(on, on ? grav : noGrav);
                }
            }
        }*/

    }
}
