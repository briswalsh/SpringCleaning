using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGravity : MonoBehaviour, IPhysics {

    [Header("Angular Drag")]
    public float airDrag;
    public float floorDrag;

    [Header("Gravity")]
    public bool gravity;
    public bool canGrav;

    [Header("Vacuum")]
    public GameObject[] vacuumObj;
    public bool[] vacuumOn;
    public float vacuumMinDist;

    /* Private Variables */
    private Rigidbody rb;
    private bool colliding;
    private Vector3 prevPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0f, -10f, 0f);

        for (int i = 0; i < vacuumOn.Length; i++)
        {
            vacuumOn[i] = false;
        }

        if(!canGrav)
        {
            Invoke("DisableGravity", 0.5f);
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

        Move();
    }

    /* Interface Implementation */

    public void Hit(float force, Vector3 dir)
    {
    }


    public void GravityControl(bool on, float gravConst)
    {
        if (canGrav)
        {
            gravity = on;
            Physics.gravity = new Vector3(0f, -gravConst, 0f);
            if (!on)
            {
                rb.AddTorque(new Vector3(Random.value, Random.value, Random.value).normalized);
            }
        }
    }

    public void Move()
    {
        Vector3 newPos = new Vector3();
        for (int i = 0; i < vacuumObj.Length; i++)
        {
            if (vacuumOn[i])
            {
                var vp = vacuumObj[i].GetComponent<VacuumPhysics>();
                if (vp != null)
                {
                    Vector3 dir = vacuumObj[i].transform.position - transform.position;
                    float dist = dir.magnitude;
                    if (dist < vp.vacuumDist)
                    {
                        newPos += vp.vacuumStr * dir.normalized / (Mathf.Max(dist * dist, vacuumMinDist));
                    }
                }
            }
        }

        prevPos = transform.position;


        RaycastHit hit;
        Physics.Raycast(transform.position, newPos, out hit, newPos.magnitude);
        if (hit.collider == null || hit.collider.tag != "Wall")
        {
            //move to newPos
            transform.position = transform.position + newPos;
        }
    }

    private void DisableGravity ()
    {
        rb.useGravity = false;
    }
}
