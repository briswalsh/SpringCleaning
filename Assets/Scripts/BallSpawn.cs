using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour {

    public GameObject movable;

    public GameObject ball;
    public Vector3 origin;

    private GameObject currBall;

	// Use this for initialization
	void Start () {
        movable = GameObject.FindGameObjectWithTag("Movable");
        currBall = Instantiate(ball, origin, new Quaternion(), movable.transform);
	}
	
	// Update is called once per frame
	void Update () {
		if (currBall == null)
        {
            currBall = Instantiate(ball, origin, new Quaternion(), movable.transform);
        }
    }
}
