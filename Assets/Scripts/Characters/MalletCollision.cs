using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalletCollision : MonoBehaviour {
    private const float slow = 6.0f;
    private const float medium = 10.0f;
    private const float fast = 15.0f;


    private Vector3[] positions;

    private float time;
    private int frame;

	// Use this for initialization
	void Start ()
    {
        frame = 0;
        time = 0.0f;
        positions = new Vector3[10];

        var tempPos = transform.position;
        tempPos.y = 0.0f;
        InitializeArray(tempPos);
	}

    void InitializeArray(Vector3 pos)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = pos;
        }
    }

    /*
     *  Shifts all values of the array back and then places the new position at the 
     *  first index.
     */
    void ShiftArray(Vector3 newPosition)
    {
        var lastPos = newPosition;
        for (int i = 0; i < positions.Length; i++)
        {
            var tempPos = lastPos;
            lastPos = positions[i];
            positions[i] = tempPos;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        time = Time.deltaTime;
        var tempPos = transform.position;
        tempPos.y = 0.0f;
        // adjust to new mallet position
        ShiftArray(tempPos);
    }

    public Vector3 GetDirection (Vector3 spherePos)
    {
        var sumDirections = spherePos - positions[0];
        for (int i = 0; i < (positions.Length - 1); i++)
        {
            sumDirections += positions[i] - positions[i + 1];
        }

        var averageDirection = sumDirections / ((float)(positions.Length + 1));
        return averageDirection;
    }

    public float GetSpeed ()
    {
        var distance = Vector3.Distance(positions[1], positions[0]);
        var speed = (float)distance / time;

        if (speed >= 14f)
        {
            speed = fast;
        }
        else if (speed >= 9f)
        {
            speed = medium;
        }
        else if (speed >= 6f)
        {
            speed = slow;
        }
        print("speed: " + speed);
        return speed;
    }
}
