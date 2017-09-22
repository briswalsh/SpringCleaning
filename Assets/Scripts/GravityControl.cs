﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour {

    public GameObject movable;

    public bool on;
    public float gravConst;

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
            /*if (on)
            {
                hum.Stop();
                hum.Play();
            } */
        }

        for(int i = 0; i < movable.transform.childCount; i++)
        {
            var sphere = movable.transform.GetChild(i).GetComponent<IPhysics>();
            if(sphere != null)
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
}
