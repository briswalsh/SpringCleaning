using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumPhysics : MonoBehaviour {

    [Header("Vacuum")]
    public bool vacuumSwitch;
    public float vacuumStr;
    public float margin;

    private bool vacuumOn;

    [Header("Reset")]
    public bool reset;

    private GameObject movable;

	// Use this for initialization
	void Start () {
        movable = GameObject.FindGameObjectWithTag("Movable");
	}
	
	// Update is called once per frame
	void Update () {
        if (movable != null)
        {
            if(vacuumSwitch != vacuumOn)
            {
                VacuumControl(vacuumSwitch);
            }
            if (vacuumOn)
            {
                int len = movable.transform.childCount;
                for (int i = 0; i < len; i++)
                {
                    Pull(movable.transform.GetChild(i));
                }
            }
        }

        if(reset)
        {
            Reset();
            reset = false;
        }
	}

    void Pull(Transform obj)
    {
        Vector3 dir = transform.position - obj.position;
        if (dir.magnitude > margin) {
            obj.position += dir.normalized * vacuumStr / (dir.magnitude * dir.magnitude);
        }
    }

    void VacuumControl(bool on)
    {
        if(on && !vacuumOn)
        {
            vacuumOn = true;
            vacuumSwitch = true;
        }
        else if (!on && vacuumOn)
        {
            vacuumOn = false;
            vacuumSwitch = false;
        }
    }

    public void Reset()
    {
        vacuumOn = true;
        VacuumControl(false);
    }
}