using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Controller { Left, Right }

public class HapticController : MonoBehaviour
{
    private List<OVRHapticsClip> clips = new List<OVRHapticsClip>();

    public void Add(AudioClip clip, Controller cont)
    {
        if (cont == 0)
        {
            clips.Add(new OVRHapticsClip(clip, 0));
        }
        else
        {
            clips.Add(new OVRHapticsClip(clip, 1));
        }
    }

    public void PlaySound(string id, Controller controller)
    {
        var channel = 0;
        if (controller == Controller.Right) channel = 1;
        var clip = LocateClip(id);
        OVRHaptics.Channels[channel].Mix(clip);
    }

    public OVRHapticsClip LocateClip(string id)
    {
        return null;
    }
}