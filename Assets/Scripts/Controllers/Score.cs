using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	private int ballsRemaining;
	Text ballsRemainingText;
	private int ballCount;

	// Use this for initialization
	void Start () {
		ballsRemainingText = GetComponent<Text> ();

		ballCount = GetComponent<BallSpawn>().ballCount;
		ballsRemainingText.text = "Balls Remaining : " + ballCount;
	}
	
	// Update is called once per frame
	void Update () {
		ballCount = GetComponent<BallSpawn>().ballCount;
		ballsRemainingText.text = "Balls Remaining : " + ballCount; 
	}
}
