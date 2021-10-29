using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RainTransition : MonoBehaviour
{
    public AudioMixerSnapshot mySnapshot;
    public GameObject outsideRain = null;
    private void Start()
    {
        if (outsideRain != null)
        {
            outsideRain.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.TransitionToSnapshot(mySnapshot, 1f);
        }
        GameObject[] goList = GameObject.FindGameObjectsWithTag("Rain");
        if (goList != null)
        {
            foreach (GameObject go in goList)
            {
                go.SetActive(false);
            }
        }
        if (outsideRain != null)
        {
            outsideRain.SetActive(true);
        } 
    }
}
