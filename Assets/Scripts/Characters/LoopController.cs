using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopController : MonoBehaviour {
    // These objects must be sphere men
    public GameObject[] spheremen;
    private Vector3[] startingPositions;

    public float timeInterval;
    private float time;

    // Use this for initialization
    void Start ()
    {
        time = 0.0f;
        startingPositions = new Vector3[spheremen.Length];
        for (int i = 0; i < spheremen.Length; i++)
        {
            var position = spheremen[i].transform.position;
            startingPositions[i] = new Vector3(position.x, position.y, position.z);
        }
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > timeInterval)
        {
            ResetPositions();
        }
	}

    void ResetPositions()
    {
        if (startingPositions.Length != spheremen.Length)
        {
            // Error occured
            throw new System.Exception("Starting positions not initialized");
        }

        for (int i = 0; i < spheremen.Length; i++)
        {
            var newPos = startingPositions[i];
            spheremen[i].transform.position = new Vector3(newPos.x, newPos.y, newPos.z);
        }
    }
}
