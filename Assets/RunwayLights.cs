using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RunwayLights : MonoBehaviour {

    public GameObject[] lights;

    public float advTime = 0.2f;
    public float pauseTime = 2.0f;

    private int lightIdx = 0;
    private float timeToOn;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(false);
        }
        timeToOn = advTime;
    }
	
	// Update is called once per frame
	void Update () {
		if (timeToOn <= 0f)
        {
            if(lightIdx < lights.Length)
            {
                lights[lightIdx].SetActive(true);
                lightIdx++;
                timeToOn = advTime;
            }
            else if(lightIdx == lights.Length)
            {
                timeToOn = pauseTime;
                lightIdx++;
            }
            else if(lightIdx == lights.Length + 1)
            {
                for (int i = 0; i < lights.Length; i++)
                {
                    lights[i].SetActive(false);
                }
                timeToOn = advTime;
                lightIdx = 0;
            }
        }
        else
        {
            timeToOn -= Time.deltaTime;
        }
	}
}
