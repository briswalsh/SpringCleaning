using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour {

	public float fadeInConst;
	public float fadeOutConst;
	public Color forceTransparent;
	public Color forceFull;

	private Color transparent;
	private Color full;

	private Text text;
	private bool flicker;
	private float t;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();

		if (forceFull == Color.clear) {
			full = text.color;
		} else {
			full = forceFull;
		}

		if (forceTransparent == Color.clear) {
			transparent = full;
			transparent.a = 0;
		} else {
			transparent = forceTransparent;
		}

		t = 0;
	}

	// Update is called once per frame
	void Update () {
		if (flicker) {
			text.color = Color.Lerp (transparent, full, t);
			t += Time.deltaTime / fadeInConst;
			if (t > 1) {
				flicker = false;
				t = 0;
			}
		} else {
			text.color = Color.Lerp (full, transparent, t);
			t += Time.deltaTime / fadeOutConst;
			if (t > 1) {
				flicker = true;
				t = 0;
			}
		}
	}
}
