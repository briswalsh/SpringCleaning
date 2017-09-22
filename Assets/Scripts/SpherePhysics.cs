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
    public float gravityStr;

    [Header("Vacuum")]
    public GameObject[] vacuumObj;
    public bool[] vacuumOn;


    [Header("Categories")]
    public GameObject spaced;


    /* Private Variables */
    private Rigidbody rb;
    private bool colliding;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0f, -10f, 0f);
        origin = transform.position;

        for(int i = 0; i < vacuumOn.Length; i++)
        {
            vacuumOn[i] = false;
        }
	}

	// Update is called once per frame
	void Update () {
//        Debug.Log(rb.velocity);

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


    /* Helper Functions */

    private void AddForce()
    {
        rb.AddForce(new Vector3(-1f, 0f) * constant);
    }

    private void AddForce(float force, Vector3 dir)
    {
        rb.AddForce(new Vector3(dir.x ,0, dir.z).normalized * force);
    }

    void ResetPosition()
    {
        transform.position = origin;
        rb.angularVelocity = new Vector3();
        rb.velocity = new Vector3();
    }


    /* Interface Implementation */

    public void Hit(float force, Vector3 dir)
    {
        AddForce(force, dir);
    }

    public void Reset()
    {
        ResetPosition();
    }

    public void GravityControl(bool on, float gravConst)
    {
        gravity = on;
        Physics.gravity = new Vector3(0f, -gravConst, 0f);
    }

    public void VacuumControl(bool on, int vacuumNum)
    {
        vacuumOn[vacuumNum] = on;
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

    private void OnTriggerEnter(Collider other)
    {
        var mc = other.gameObject.GetComponent<MalletCollision>();

        if(mc != null)
        {
            Hit(mc.GetSpeed() * constant, mc.GetDirection(transform.position));
        }

		var wc = other.gameObject.GetComponent<WicketCollision> ();

		if (wc != null) {
			print ("Found Wicket");
            transform.SetParent(spaced.transform);
            wc.Eject (gameObject);
		} 

		else {
			print ("Could not find Wicket");
		}
    }
}
