using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherePhysics : MonoBehaviour, IPhysics {

    [Header("Adding Force")]
    public float constant;
    public bool triggerForce;

    [Header("Angular Drag")]
    public float airDrag;
    public float floorDrag;

    [Header("Reset")]
    public bool reset;
    private Vector3 origin;

    [Header("Gravity")]
    public bool gravity;
    public float gravityValue;

    private Rigidbody rb;
    private bool colliding;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0f, -10f, 0f);
        origin = transform.position;
	}

	// Update is called once per frame
	void Update () {
        if (triggerForce)
        {
            AddForce();
            triggerForce = false;
        }

        if (reset)
        {
            ResetPosition();
            reset = false;
        }

        if (gravity)
        {
            if (colliding)
            {
                rb.angularDrag = floorDrag;
                Physics.gravity = new Vector3(0f, -10f, 0f);
            }
            else
            {
                rb.angularDrag = airDrag;
                Physics.gravity = new Vector3(0f, -10f, 0f);
            }
        }
        else
        {
            if (colliding)
            {
                rb.angularDrag = floorDrag;
                Physics.gravity = new Vector3();
            }
            else
            {
                rb.angularDrag = airDrag;
                Physics.gravity = new Vector3();
            }
        }
        gravityValue = Physics.gravity.y;
    }

    /* Helper Functions */

    private void AddForce()
    {
        rb.AddForce(new Vector3(-1f, 0f) * constant);
    }

    private void AddForce(int force, Vector3 dir)
    {
        rb.AddForce(dir.normalized * force);
    }

    void ResetPosition()
    {
        transform.position = origin;
        rb.angularVelocity = new Vector3();
        rb.velocity = new Vector3();
    }

    /* Interface Implementation */

    public void Hit(int force, Vector3 dir)
    {
        AddForce(force, dir);
    }

    public void Reset()
    {
        ResetPosition();
    }

    public void GravityControl(bool on)
    {
        gravity = on;
    }

    /* Collision Detectors */
    private void OnCollisionStay(Collision collision)
    {
        colliding = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        colliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        colliding = false;
    }
}
