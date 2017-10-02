using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePhysics : MonoBehaviour {

    public bool[] vacuumOn;
    public GameObject[] vacuumObj;

	// Use this for initialization
	void Awake () {
        vacuumObj = GameObject.FindGameObjectsWithTag("Vacuum");
        vacuumOn = new bool[vacuumObj.Length];
        for(int i = 0; i < vacuumObj.Length; i++)
        {
            vacuumOn[i] = vacuumObj[i].GetComponent<VacuumPhysics>().vacuumSwitch;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
