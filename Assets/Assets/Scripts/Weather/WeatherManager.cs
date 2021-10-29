using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public float lightningInterval;
    private float lightningTimer = 0f;
    public Transform raycastSource;
    public LayerMask coverMask;
    public ParticleSystem rain;
    public AudioClip rainLoop;
    public AudioClip thunderSFX;
    public float raycastLength;
    public float thunderInterval;
    private bool waiting;
    //public Transform thunderPosition;
    private GameObject rainGO;
    //private bool isRaining = false;
    //TEST
    private void Start()
    {
        lightningTimer = 0f;
        waiting = true;
        AudioManager.TransitionToSnapshot(AudioManager.theManager.Exterior, 0f);
        rainGO = SFXManager.sfxManager.LoopAmbience(rainLoop);
        RainToggle(RainCheck());
    }
    private bool RainCheck()
    {
        if (rain.isPlaying)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void RainToggle(bool b)
    {
        if (b)
        {
            Debug.Log("Rain TRUE");
            AudioManager.TransitionToSnapshot(AudioManager.theManager.Exterior, 0.2f);
            rain.Play();
        }
        else
        {
            Debug.Log("Rain FALSE");
        //    AudioManager.TransitionToSnapshot(AudioManager.theManager.Interior, 0.2f);
            rain.Stop();
        }
    }
    private void Update()
    {
        if (rain.gameObject.activeInHierarchy && rain.isPlaying)
        {
            Transform t = PlayerController.thePlayer.transform;
            rain.transform.position = new Vector3(t.position.x, rain.transform.position.y, t.position.z);
        }
        RaycastHit hit;
        if (Physics.Raycast(raycastSource.position, Vector3.up, out hit, raycastLength, coverMask))
        {
            if (RainCheck())
            {
                RainToggle(false);
            }
        }
        else
        {
            if (!RainCheck())
            {
                RainToggle(true);
            }
        }
        if (waiting)
        {
            lightningTimer += Time.deltaTime;
            if (lightningTimer >= lightningInterval)
            {
                Lightning();
            }
        }
    }
    private void Lightning()
    {
        waiting = false;
        StartCoroutine(Thunder());
    }
    private IEnumerator Thunder()
    {
        yield return new WaitForSeconds(thunderInterval);
        SFXManager.sfxManager.PlayOneShot(thunderSFX, AudioManager.theManager.Lightning);
        StartCoroutine(LightningReset());
    }
    private IEnumerator LightningReset()
    {
        yield return new WaitForSeconds(thunderSFX.length);
        lightningTimer = 0f;
        waiting = true;
    }
}
