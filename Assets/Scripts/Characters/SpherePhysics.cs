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
    public float vacuumMinDist;

    [Header("Categories")]
    public GameObject movable;
    public GameObject malletMan;

    /* Private Variables */
    private Rigidbody rb;
    private bool colliding;
    private Vector3 prevPos;
    private Vector3 vel;
    private bool physics;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        //Physics.gravity = new Vector3(0f, -10f, 0f);
        origin = transform.position;
        physics = true;
        prevPos = transform.position;
        vel = new Vector3();

        movable = GameObject.FindGameObjectWithTag("Movable");
        vacuumObj = movable.GetComponent<BallSpawn>().vacuumObj;
        vacuumOn = movable.GetComponent<BallSpawn>().vacuumOn;

        malletMan = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreCollision(GetComponent<Collider>(), malletMan.GetComponent<Collider>());
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (physics)
        {
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

            if (colliding)
            {
                rb.angularDrag = floorDrag;
            }
            else
            {
                if (gravity)
                {
                    rb.angularDrag = 0f;
                }
                else
                {
                    rb.angularDrag = airDrag;
                }
            }

            Move();
        }
        else
        {
            transform.position += vel * Time.deltaTime;
        }
    }


    /* Helper Functions */

    private void AddForce()
    {
        rb.AddForce(new Vector3(0f, 0f, -1f) * constant);
    }

    private void AddForce(float force, Vector3 dir)
    {
        rb.AddForce(new Vector3(dir.x ,0, dir.z).normalized * force);
    }

    private void ResetPosition()
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

    public void GravityControl(bool on, float gravConst)
    {
        gravity = on;
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
                        newPos += vp.vacuumStr * dir.normalized / (Mathf.Max(dist * dist, vacuumMinDist * vacuumMinDist));
                        print(newPos);
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

        var vp = other.gameObject.GetComponent<VacuumPhysics>();
        if (vp != null)
        {
            vp.vacuumSwitch = false;

            var wc = other.gameObject.GetComponent<WicketCollision>();

            if (wc != null)
            {
                physics = false;
                vel = vp.transform.position - transform.position;
                rb.useGravity = false;

                StartCoroutine(wc.Eject(gameObject, other.gameObject));
            }
        }
        

		else {
            print(other.name);
			print ("Could not find Wicket");
		}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Safe Area")
        {
            rb.useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        rb.useGravity = true;
    }
}
