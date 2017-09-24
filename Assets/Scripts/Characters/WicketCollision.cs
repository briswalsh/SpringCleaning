using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WicketCollision : MonoBehaviour {

	private float time;
	private GameObject sph;

	public void Eject(GameObject sphere) {
		
		time = Time.time; // time when ball hits the wicket
		sph = sphere;
		//var score = GameObject.Find ("Player").GetComponent<Score> ();
		//score.addScore ();
		//score.showScore ();

	}
		

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (sph != null) {
			if (Time.time < (time + 2)) { // before 5 seconds pass
				// Once the ball hits the wicket, force is applied to the ball for 5 seconds
				Rigidbody rb = sph.GetComponent<Rigidbody> ();
				rb.AddForce (transform.forward);
			} else {
				// After 5 seconds, the force is not applied anymore and the ball becomes invisible
				// sph.GetComponent<MeshRenderer> ().enabled = false;
				Destroy(sph);
			}
		}
	}
}
