using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    public Dictionary<string, AudioClip> audioLibrary;
    public List<AudioClip> audioClips;
    public AudioListener AL;
    public AudioMixerGroup ambience;
    public AudioMixerGroup sfx;

    public GameObject SFXPrefab;

    public static SFXManager sfxManager;
    public AudioMixer audioMixer;

    private void Awake()
    {
        sfxManager = this;
        audioLibrary = new Dictionary<string, AudioClip>();

        foreach (AudioClip item in audioClips)
        {
            audioLibrary.Add(item.name, item);
        }
    }
    public void PlaySFX(string s)
    {
        StartCoroutine(SFX(s));
    }
    private IEnumerator SFX(string name)
    {
        GameObject GO = Instantiate(SFXPrefab);
        AudioSource AS = GO.transform.GetComponent<AudioSource>();
        AS.clip = audioLibrary[name];
        AS.Play();
        yield return new WaitForSeconds(audioLibrary[name].length);
        Destroy(GO);
    }
    public void PlaySFX(AudioClip ac)
    {
        StartCoroutine(SFX(ac));
    }
    private IEnumerator SFX(AudioClip ac)
    {
        GameObject GO = Instantiate(SFXPrefab);
        AudioSource AS = GO.transform.GetComponent<AudioSource>();
        AS.clip = ac;
        AS.Play();
        yield return new WaitForSeconds(ac.length);
        Destroy(GO);
    }
    public void PlaySFX(AudioClip ac, Transform t)
    {
        StartCoroutine(SFX(ac, t));
    }
    private IEnumerator SFX(AudioClip ac, Transform t)
    {
        GameObject GO = Instantiate(SFXPrefab, t.position, Quaternion.identity);
        AudioSource AS = GO.transform.GetComponent<AudioSource>();
        AS.clip = ac;
        AS.Play();
        yield return new WaitForSeconds(ac.length);
        Destroy(GO);
    }
    public GameObject LoopAmbience(AudioClip ac)
    {
        GameObject GO = Instantiate(SFXPrefab);
        AudioSource AS = GO.transform.GetComponent<AudioSource>();
        AS.outputAudioMixerGroup = ambience;
        AS.clip = ac;
        AS.loop = true;
        AS.Play();
        return GO;
    }
    public void PlayOneShot(AudioClip ac, AudioMixerGroup amg)
    {
        StartCoroutine(OneShot(ac, amg));
    }
    private IEnumerator OneShot(AudioClip ac, AudioMixerGroup amg)
    {
        GameObject GO = Instantiate(SFXPrefab);
        AudioSource AS = GO.transform.GetComponent<AudioSource>();
        AS.clip = ac;
        AS.outputAudioMixerGroup = amg;
        AS.Play();
        yield return new WaitForSeconds(ac.length);
        Destroy(GO);
    }
}
