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

    private Rigidbody rb;
    private bool colliding;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0f, -5f, 0f);
        origin = transform.position;
	}

	// Update is called once per frame
	void Update () {
        if (triggerForce)
        {
            AddForce();
        }

        if (reset)
        {
            ResetPosition();
        }

        if (gravity)
        {
            if (colliding)
            {
                rb.angularDrag = floorDrag;
                Physics.gravity = new Vector3(0f, -5f, 0f);
            }
            else
            {
                rb.angularDrag = airDrag;
                Physics.gravity = new Vector3(0f, -10f, 0f);
            }
        }
        else
        {
            Physics.gravity = new Vector3();
        }
    }

    private void AddForce()
    {
        rb.AddForce(new Vector3(-1f, 0f) * constant);
        triggerForce = false;
    }

    private void AddForce(int force)
    {
        rb.AddForce(new Vector3(-1f, 0f) * force);
        triggerForce = false;
    }

    void ResetPosition()
    {
        transform.position = origin;
        rb.angularVelocity = new Vector3();
        rb.velocity = new Vector3();
        reset = false;
    }

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

    public void Hit(int force)
    {
        AddForce(force);
    }

    public void Reset()
    {
        ResetPosition();
    }
}
