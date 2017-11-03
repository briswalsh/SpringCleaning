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
        black.color = transparent;
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two) ||
            OVRInput.GetDown(OVRInput.Button.Three) || OVRInput.GetDown(OVRInput.Button.Four) ||
            (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch) > 0) ||
            (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.Touch) > 0)||
            (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) > 0) ||
            (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.Controller.Touch) > 0) ||
            OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick) ||
            OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick) ||
            OVRInput.GetDown(OVRInput.Button.Start))
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
