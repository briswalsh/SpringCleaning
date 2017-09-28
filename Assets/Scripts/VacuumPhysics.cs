using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumPhysics : MonoBehaviour {

    [Header("Vacuum")]
    public bool vacuumSwitch;
    public float vacuumStr;
    public float vacuumDist;

    private bool vacuumOn;

    private GameObject movable;
    public int movablePos;

	// Use this for initialization
	void Start () {
        movable = GameObject.FindGameObjectWithTag("Movable");
        var mp = movable.GetComponent<MovablePhysics>();
        for (int i = 0; i < mp.vacuumObj.Length; i++)
        {
            if (mp.vacuumObj[i] == gameObject)
            {
                movablePos = i;
                break;
            }
        }
        print(movablePos);

	}
	
	// Update is called once per frame
	void Update () {
        if (movable != null)
        {
            if (vacuumSwitch != vacuumOn)
            {
                VacuumControl(vacuumSwitch);
            }
        }
	}

    void VacuumControl(bool on)
    {
        if(on && !vacuumOn)
        {
            vacuumOn = true;
            vacuumSwitch = true;
            movable.GetComponent<MovablePhysics>().vacuumOn[movablePos] = true;
        }
        else if (!on && vacuumOn)
        {
            vacuumOn = false;
            vacuumSwitch = false;
            movable.GetComponent<MovablePhysics>().vacuumOn[movablePos] = false;
        }
    }
}