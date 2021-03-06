﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour {
	private AudioSource ambient;

    private AudioSource oneOffManager;

    private AudioSource narrator;

	private AudioSource bgm;

    private Dictionary<string, AudioClip> sounds;

	private bool loop;
	private Vector3 loopLocation;
    private Vector3 findLocation;
	private AudioClip distantLoop;
	private float loopTimer;
	private float loopTime;
    private float sfxVol;

	public int channel;
    public GameObject player;
  
   

    // Use this for initialization
    void Awake ()
    {
        sounds = new Dictionary<string, AudioClip>();
        oneOffManager = GetComponents<AudioSource>()[0];
        oneOffManager.loop = false;
        ambient = GetComponents<AudioSource>()[1];
        ambient.loop = true;
		loop = false;
		ambient.Play ();
        narrator = GetComponents<AudioSource>()[2];
        narrator.playOnAwake = false;
        narrator.loop = false;
		bgm = GetComponents<AudioSource> () [3];
		bgm.loop = true;
		bgm.volume = 0.05f;
		bgm.Play ();

        sfxVol = 0.5f;
        oneOffManager.volume = sfxVol;

//        var boxHit = Resources.Load("ball-box", typeof(AudioClip)) as AudioClip;
        var malletHit = Resources.Load("ball-mallet", typeof(AudioClip)) as AudioClip;
        var boilerHum = Resources.Load("Boiler Hum", typeof(AudioClip)) as AudioClip;
//        var boilerHumRedo = Resources.Load("Boiler Hum redone", typeof(AudioClip)) as AudioClip;
        var gravDown = Resources.Load("Grav Down", typeof(AudioClip)) as AudioClip;
		var gravUp = Resources.Load("Grav Up", typeof(AudioClip)) as AudioClip;
//        var gravityDown = Resources.Load("Gravity Down", typeof(AudioClip)) as AudioClip;
        var impact = Resources.Load("Impact", typeof(AudioClip)) as AudioClip;
        var pneumatic = Resources.Load("pneumatics_up-loud", typeof(AudioClip)) as AudioClip;
		var comet = Resources.Load ("comet v1", typeof(AudioClip)) as AudioClip;
//        var SpaceEngineStart = Resources.Load("SpaceEngine_Start_00", typeof(AudioClip)) as AudioClip;
        var torch = Resources.Load("torch", typeof(AudioClip)) as AudioClip;
		var torchMuffled = Resources.Load("torch scream muffled", typeof(AudioClip)) as AudioClip;
		var torchScream = Resources.Load("torch scream full", typeof(AudioClip)) as AudioClip;
        var vacuum = Resources.Load("Vaccumn loop", typeof(AudioClip)) as AudioClip;
        var vacIntro = Resources.Load("vacumn intro", typeof(AudioClip)) as AudioClip;
//        var whiteNoise = Resources.Load("White noise", typeof(AudioClip)) as AudioClip;
        var wicketDing = Resources.Load("Wicket ding", typeof(AudioClip)) as AudioClip;
        var wicketOpen = Resources.Load("Wicket open", typeof(AudioClip)) as AudioClip;
        var winMusic = Resources.Load("WinMusic", typeof(AudioClip)) as AudioClip;
		var elevator = Resources.Load ("elevator_music", typeof(AudioClip)) as AudioClip;

   		//Narration Lines
		var narIntroOne = Resources.Load("Improved Intro Voiceover P1", typeof(AudioClip)) as AudioClip;
		var narIntroTwo = Resources.Load("Improved Intro Voiceover P2", typeof(AudioClip)) as AudioClip;
		var narFirstFail= Resources.Load("Voiceover Narration 7 Missed Hit WarningB", typeof(AudioClip)) as AudioClip;
 //		var narFailOne = Resources.Load("", typeof(AudioClip)) as AudioClip;
 //		var narFailTwo = Resources.Load("", typeof(AudioClip)) as AudioClip;
		var narGravOn = Resources.Load("Improved Grav Intro Correct Space", typeof(AudioClip)) as AudioClip;
		var narGravFail = Resources.Load("Improved Grav Fail", typeof(AudioClip)) as AudioClip;
		var narSuckOn = Resources.Load("Improved Vacum Intro", typeof(AudioClip)) as AudioClip;
		var narSuckFail = Resources.Load("Improve Vacum loose", typeof(AudioClip)) as AudioClip;
		var narLose = Resources.Load("Voiceover Narration 6 LossB", typeof(AudioClip)) as AudioClip;
		var narWin = Resources.Load("Voiceover Narration 5 WinB", typeof(AudioClip)) as AudioClip;

        // add the sounds to a dictionary
//        sounds.Add("ball-box", boxHit);
        sounds.Add("ball-mallet", malletHit);
        sounds.Add("boiler-hum1", boilerHum);
//        sounds.Add("boiler-hum2", boilerHumRedo);
        sounds.Add("grav-down", gravDown);
		sounds.Add("grav-up", gravUp);
//        sounds.Add("gravity-down", gravityDown);
        sounds.Add("impact", impact);
        sounds.Add("pneumatic", pneumatic);
		sounds.Add ("comet", comet);
//		sounds.Add("space-engine-start", SpaceEngineStart);
        sounds.Add("torch", torch);
		sounds.Add ("torch-muffled", torchMuffled);
		sounds.Add("torch-scream", torchScream);
        sounds.Add("vacuum", vacuum);
        sounds.Add("vacuum-start", vacIntro);
//        sounds.Add("white-noise", whiteNoise);
        sounds.Add("wicket-ding", wicketDing);
        sounds.Add("wicket-open", wicketOpen);
        sounds.Add("win-music", winMusic);
		sounds.Add ("elevator", elevator);


		sounds.Add ("intro1", narIntroOne);
 		sounds.Add ("intro2", narIntroTwo);
 		sounds.Add ("firstFail", narFirstFail);
//		sounds.Add ("fail1", narFailOne);
//		sounds.Add ("fail2", narFailTwo);
		sounds.Add ("gravOn", narGravOn);
		sounds.Add ("gravFail", narGravFail);
		sounds.Add ("suckOn", narSuckOn);
		sounds.Add ("suckFail", narSuckFail);
		sounds.Add ("loseGame", narLose);
		sounds.Add ("winGame", narWin);


    }
	
	// Update is called once per frame
	void Update ()
    {
		if(loop && oneOffManager.enabled)
		{
			loopTime -= Time.deltaTime;
			if(loopTime < 0)
			{
                for (int i = 0; i < 1; i++)
                {
                    AudioSource.PlayClipAtPoint(distantLoop, player.transform.position);
                }
				loopTime = loopTimer;
			}
		}
	}

    public void PlaySound(string soundId)
    {
        try
        {
            oneOffManager.enabled = true;
            var sound = sounds[soundId];
            oneOffManager.PlayOneShot(sound);
        }
        catch
        {
            throw new System.Exception("Could not locate sound - " + soundId);
        }
    }

	public void PlayDirectionalSound(string soundId, Vector3 location)
	{
		try
		{
			var sound = sounds[soundId];
            findLocation = location;// - player.transform.position;
            //findLocation.Normalize();
			AudioSource.PlayClipAtPoint(sound, findLocation);
		}
		catch
		{
			throw new System.Exception("Could not locate sound - " + soundId);
		}
	}

	public void PlayDirectionalLoop(string soundId, Vector3 location)
	{
		try
		{
			var sound = sounds[soundId];

			oneOffManager.clip = sound;
            findLocation = location;// - player.transform.position;
			//Vector3.Normalize(findLocation);
			loopLocation = findLocation;
			loop = true;
			distantLoop = sound;
			loopTimer = sound.length;
			loopTime = sound.length;

		}
		catch
		{
			throw new System.Exception("Could not locate sound - " + soundId);
		}
	}

	public void StopLoop()
	{
		oneOffManager.Stop ();
	}

    public void StopSoundLoop()
    {
        loop = false;
    }

    public void Vibrate(string soundId, int channel)
    {
        var clip = sounds[soundId];
        var hapClip = new OVRHapticsClip(clip, channel);
        OVRHaptics.Channels[channel].Mix(hapClip);
    }
		
    public void Win()
    {
        ambient.Stop();
        ambient.clip = sounds["win-music"];
        ambient.PlayDelayed(9.5f);
		ambient.volume = .8f;
    }

    public void Cut()
    {
        ambient.Stop();
        narrator.Stop();
        oneOffManager.Stop();
        oneOffManager.enabled = false;
		bgm.Stop ();
    }

    public bool IsNarrating() {
		return narrator.isPlaying;
	}

    public void Narrate(string soundId)
    {
        try
        {
            if (narrator.isPlaying)
            {
                narrator.Stop();
            }

            var sound = sounds[soundId];

            
            narrator.PlayOneShot(sound);
        }
        catch
        {
            throw new System.Exception("Could not locate sound - " + soundId);
        }
    }
}
