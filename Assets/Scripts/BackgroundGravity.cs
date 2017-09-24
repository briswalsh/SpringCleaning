using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGravity : MonoBehaviour, IPhysics {

    [Header("Force")]
    public float constant;

    [Header("Angular Drag")]
    public float airDrag;
    public float floorDrag;

    [Header("Gravity")]
    public bool gravity;

    [Header("Vacuum")]
    public GameObject[] vacuumObj;
    public bool[] vacuumOn;


    [Header("Categories")]
    public GameObject spaced;


    /* Private Variables */
    private Rigidbody rb;
    private bool colliding;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0f, -10f, 0f);

        for (int i = 0; i < vacuumOn.Length; i++)
        {
            vacuumOn[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gravity)
        {
            if (colliding)
            {
                rb.angularDrag = floorDrag;
            }
            else
            {
                rb.angularDrag = airDrag;
            }
        }
        else
        {
            if (colliding)
            {
                rb.angularDrag = floorDrag;
            }
            else
            {
                rb.angularDrag = airDrag;
            }
        }
    }

    /* Interface Implementation */

    public void Hit(float force, Vector3 dir)
    {
    }


    public void GravityControl(bool on, float gravCost)
    {
        gravity = on;
        if (!on)
        {
            Physics.gravity = new Vector3(0f, 0.3f, 0f);
        }

        else
        {
            Physics.gravity = new Vector3(0f, -gravCost, 0f);
        }
    }

    public void VacuumControl(bool on, int vacuumNum)
    {
    }
}
