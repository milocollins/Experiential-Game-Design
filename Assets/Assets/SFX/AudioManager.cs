using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager theManager;
    public AudioMixer AM;
    public AudioMixerSnapshot Interior;
    public AudioMixerSnapshot Exterior;
    public AudioMixerGroup Lightning;
    private void Awake()
    {
        theManager = this;
    }
    public static void TransitionToSnapshot(AudioMixerSnapshot ams, float t)
    {
        ams.TransitionTo(t);
    }
}
