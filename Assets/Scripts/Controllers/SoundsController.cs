using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour {
	private AudioSource ambient;

    private AudioSource oneOffManager;

    private Dictionary<string, AudioClip> sounds;

	private bool loop;
	private Vector3 loopLocation;
	private AudioClip distantLoop;
	private float loopTimer;
	private float loopTime;

	public int channel;

	// Use this for initialization
	void Start ()
    {
        sounds = new Dictionary<string, AudioClip>();
        oneOffManager = GetComponents<AudioSource>()[0];
        oneOffManager.loop = false;
        ambient = GetComponents<AudioSource>()[1];
        ambient.loop = true;
		loop = false;
		ambient.Play ();

        var boxHit = Resources.Load("ball-box", typeof(AudioClip)) as AudioClip;
        var malletHit = Resources.Load("ball-mallet", typeof(AudioClip)) as AudioClip;
        var boilerHum = Resources.Load("Boiler Hum", typeof(AudioClip)) as AudioClip;
        var boilerHumRedo = Resources.Load("Boiler Hum redone", typeof(AudioClip)) as AudioClip;
        var gravDown = Resources.Load("Grav Down", typeof(AudioClip)) as AudioClip;
		var gravUp = Resources.Load("Grav Up", typeof(AudioClip)) as AudioClip;
        var gravityDown = Resources.Load("Gravity Down", typeof(AudioClip)) as AudioClip;
        var impact = Resources.Load("Impact", typeof(AudioClip)) as AudioClip;
        var pneumatic = Resources.Load("pneumatics-2", typeof(AudioClip)) as AudioClip;
        var SpaceEngineStart = Resources.Load("SpaceEngine_Start_00", typeof(AudioClip)) as AudioClip;
        var torch = Resources.Load("torch", typeof(AudioClip)) as AudioClip;
        var vacuum = Resources.Load("Vaccumn loop", typeof(AudioClip)) as AudioClip;
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
		sounds.Add("grav-up", gravUp);
        sounds.Add("gravity-down", gravityDown);
        sounds.Add("impact", impact);
        sounds.Add("pneumatic", pneumatic);
		sounds.Add("space-engine-start", SpaceEngineStart);
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
		if(loop)
		{
			loopTime -= Time.time;
			if(loopTime < 0)
			{
				AudioSource.PlayClipAtPoint (distantLoop, loopLocation);
				loopTime = loopTimer;
			}

		}
	}

    public void PlaySound(string soundId)
    {
        try
        {
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
            findLocation = location - player.transform.position;
			Vector3.Normalize(findLocation);
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
            findLocation = location - player.transform.position;
			Vector3.Normalize(findLocation);
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

    public void Vibrate(string soundId, int channel)
    {
        var clip = sounds[soundId];
        var hapClip = new OVRHapticsClip(clip, channel);
        OVRHaptics.Channels[channel].Mix(hapClip);
    }
		
    public void Win()
    {
        ambient.Stop();
        oneOffManager.PlayOneShot(sounds["win-music"]);
    }
}
