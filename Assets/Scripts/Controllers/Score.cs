using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    private GameObject movable;

	private int ballsRemaining;
	private Text ballsRemainingText;
	private int ballCount;

	// Use this for initialization
	void Start () {
        movable = GameObject.FindGameObjectWithTag("Movable");

		ballsRemainingText = GetComponent<Text> ();

		ballCount = movable.GetComponent<BallSpawn>().ballCount;
        //ballsRemainingText.text = "Balls Left   " + ballCount;
        ballsRemainingText.text = "" + ballCount;
    }
	
	// Update is called once per frame
	void Update () {
		ballCount = movable.GetComponent<BallSpawn>().ballCount;
		//ballsRemainingText.text = "Balls Left   " + ballCount;
        ballsRemainingText.text = "" + ballCount;
    }
}
