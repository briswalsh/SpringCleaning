using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WicketCollision : MonoBehaviour {

    public GameObject movable;

	private float time;
	private GameObject sph;
    private Vector3 velocity;

    public IEnumerator Eject(GameObject sphere, GameObject col) {

        GameObject wall = col.transform.parent.parent.gameObject;
        if (wall.name.Contains("Wall Collider"))
        {
            wall.GetComponent<Collider>().enabled = false;
        }
        sph = sphere;
        time = Time.time;

        //var score = GameObject.Find ("Player").GetComponent<Score> ();
        //score.addScore ();
        //score.showScore ();
        yield return new WaitForSeconds(0.5f);
        Destroy(sphere);
        movable.GetComponent<BallSpawn>().NextStage();
    }


    // Use this for initialization
    void Start () {
        movable = GameObject.FindGameObjectWithTag("Movable");
    }

    // Update is called once per frame
    void Update () {
        if (sph != null) {
			if (Time.time < (time + 2)) { // before 5 seconds pass
				// Once the ball hits the wicket, force is applied to the ball for 5 seconds
				Rigidbody rb = sph.GetComponent<Rigidbody> ();
			} else {
				// After 5 seconds, the force is not applied anymore and the ball becomes invisible
				// sph.GetComponent<MeshRenderer> ().enabled = false;
				Destroy(sph);
			}
		}
	}
    //OnTriggerEnter, call Next stage function (movable.GetComponent<BallSpawn>().NextStage();)

    /* Collision Detectors */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Vacuum")
        {
            //movable.GetComponent<BallSpawn>().NextStage();
        }
    }
}
