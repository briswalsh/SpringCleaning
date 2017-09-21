using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumPhysics : MonoBehaviour {

    [Header("Vacuum")]
    public bool vacuumSwitch;
    public float vacuumStr;
    private bool vacuumOn;

    public float t;

    private GameObject movable;

	// Use this for initialization
	void Start () {
        movable = GameObject.FindGameObjectWithTag("Movable");
        t = vacuumStr;
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
	} 

    void Pull(Transform obj)
    {
        Vector3 dir = (transform.position - obj.position).normalized;
        obj.position += dir.normalized * (t + 1);
        Vector3 newpos = Vector3.Lerp(obj.position, transform.position, t * (vacuumStr + 1));
        obj.position = newpos;
        t *= vacuumStr + 1;
    }

    void VacuumControl(bool on)
    {
        if(on && !vacuumOn)
        {
            t = vacuumStr;
            vacuumOn = true;
        }
        else if (!on && vacuumOn)
        {
            t = 0f;
            vacuumOn = false;
        }
    }
}