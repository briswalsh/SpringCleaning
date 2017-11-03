using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControl : MonoBehaviour {

    public Image black; //fade screen to black
    public float duration;

    private bool transition;
    private float t;
    private Color solid;
    private Color transparent;

    // Use this for initialization
    void Start () {
        transition = false;
        t = 0;
        solid = black.color;
        transparent = solid;
        transparent.a = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two) ||
            OVRInput.GetDown(OVRInput.Button.Three) || OVRInput.GetDown(OVRInput.Button.Four))
        {
            transition = true;
        }

        if (transition)
        {
            if (t > 1)
            {
                SceneManager.LoadScene("Level1");
            }
            black.color = Color.Lerp(transparent, solid, t);
            t += Time.deltaTime / duration;
        }
    }
}
