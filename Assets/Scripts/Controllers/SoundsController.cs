﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour {
    private AudioSource hum;

    private AudioSource oneOffManager;

    private Dictionary<string, AudioClip> sounds;
    private bool playBackground = true;

	// Use this for initialization
	void Start ()
    {
        sounds = new Dictionary<string, AudioClip>();
        oneOffManager = GetComponent<AudioSource>();
        hum = GetComponent<AudioSource>();

        var boxHit = Resources.Load("ball-box", typeof(AudioClip)) as AudioClip;
        var malletHit = Resources.Load("ball-mallet", typeof(AudioClip)) as AudioClip;
        var boilerHum = Resources.Load("Boiler Hum", typeof(AudioClip)) as AudioClip;
        var boilerHumRedo = Resources.Load("Boiler Hum redone", typeof(AudioClip)) as AudioClip;
        var gravDown = Resources.Load("Grav Down", typeof(AudioClip)) as AudioClip;
        var gravityDown = Resources.Load("Gravity Down", typeof(AudioClip)) as AudioClip;
        var impact = Resources.Load("Impact", typeof(AudioClip)) as AudioClip;
        var pneumatic = Resources.Load("pneumatics-2", typeof(AudioClip)) as AudioClip;
        var engineStart = Resources.Load("SpaceEngine_Start_00", typeof(AudioClip)) as AudioClip;
        var torch = Resources.Load("torch", typeof(AudioClip)) as AudioClip;
        var vacuum = Resources.Load("Vaccumn", typeof(AudioClip)) as AudioClip;
        var vacIntro = Resources.Load("vacumn intro", typeof(AudioClip)) as AudioClip;
        var whiteNoise = Resources.Load("White noise", typeof(AudioClip)) as AudioClip;
        var wicketDing = Resources.Load("Wicket ding", typeof(AudioClip)) as AudioClip;
        var wicketOpen = Resources.Load("Wicket open", typeof(AudioClip)) as AudioClip;
        var winMusic = Resources.Load("WinMusic", typeof(AudioClip)) as AudioClip;

        // add the sounds to a dictionary
        sounds.Add("ball-box", boxHit);
        sounds.Add("ball-mallet", malletHit);
        sounds.Add("boiler-hum1", boilerHum);
        sounds.Add("boiler-hum2", boilerHumRedo);
        sounds.Add("grav-down", gravDown);
        sounds.Add("gravity-down", gravityDown);
        sounds.Add("impact", impact);
        sounds.Add("pneumatic", pneumatic);
        sounds.Add("engine-start", engineStart);
        sounds.Add("torch", torch);
        sounds.Add("vacuum", vacuum);
        sounds.Add("vacuum-start", vacIntro);
        sounds.Add("white-noise", whiteNoise);
        sounds.Add("wicket-ding", wicketDing);
        sounds.Add("wicket-open", wicketOpen);
        sounds.Add("win-music", winMusic);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void PlaySound(string soundId)
    {
        try
        {
            var sound = sounds[soundId];
            oneOffManager.PlayOneShot(sound);
        }
        catch
        {
            throw new System.Exception("Could not locate sound " + soundId);
        }
    }

    void StopSound(string soundId)
    {

    }

    void Win()
    {
        hum.Stop();
        oneOffManager.PlayOneShot(sounds["win-music"]);
    }
}