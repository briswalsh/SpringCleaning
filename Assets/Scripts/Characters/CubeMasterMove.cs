using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMasterMove : MonoBehaviour {

    public Vector3 center;
    public float radius;
    public float speed;

    public float rad;

	// Use this for initialization
	void Start () {
        transform.position = center + new Vector3(radius, 0, 0);
        rad = 0;
	}
	
	// Update is called once per frame
	void Update () {
        rad += Time.deltaTime * speed / radius;
        transform.position = center + radius * (new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)));
        

		
	}
}
