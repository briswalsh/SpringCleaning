using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour {

    public GameObject movable;

    public GameObject ball;
    public Vector3 origin;

    public bool killTrigger;

    private GameObject currBall;

	// Use this for initialization
	void Start () {
        currBall = Instantiate(ball, origin, new Quaternion(), movable.transform);
	}
	
	// Update is called once per frame
	void Update () {
		if (currBall == null)
        {
            currBall = Instantiate(ball, origin, new Quaternion(), movable.transform);
        }

        if (killTrigger)
        {
            Kill();
            killTrigger = false;
        }
    }

    public void Kill()
    {
        currBall.GetComponent<SphereDeath>().deathRow = true;
    }
}
