using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalletCollision : MonoBehaviour {
    private Vector3 lastPosition;
    private Vector3 currentPosition;
    private float time;

	// Use this for initialization
	void Start ()
    {
        time = 0.0f;
        var tempPos = transform.position;
        tempPos.y = 0.0f;
        currentPosition = tempPos;
        lastPosition = tempPos;
	}
	
	// Update is called once per frame
	void Update ()
    {
        time = Time.deltaTime;

        //TODO : Possible race-condition
        var tempPos = currentPosition;
        var tempNew = transform.position;
        // precondition : all previously set positions have a nulled y val.
        tempNew.y = 0.0f;
        currentPosition = tempNew;
        lastPosition = tempPos;
    }

    Vector3 GetDirection (Vector3 spherePos)
    {
        return spherePos - currentPosition;
    }

    float GetSpeed ()
    {
        var distance = Vector3.Distance(lastPosition, currentPosition);
        return (float) distance / time;
    }
}
