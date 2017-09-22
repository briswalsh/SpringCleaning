using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour {

    public GameObject movable;

    public bool on;

    public float duration;
    private float t;

    AudioSource hum;

	// Use this for initialization
	void Start () {
        movable = GameObject.FindGameObjectWithTag("Movable");
        t = Time.time;
        hum = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > t)
        {
            t = Time.time + duration;
            on = !on;
            if (on)
            {
                hum.Play();
            } else
            {
                hum.Stop();
            }
        }

        for(int i = 0; i < movable.transform.childCount; i++)
        {
            var sphere = movable.transform.GetChild(i).GetComponent<SpherePhysics>();
            if(sphere != null)
            {
                sphere.GravityControl(on);
            }
        }
	}
}
