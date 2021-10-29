using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager theBoss;
    public bool isPaused = false;
    private void Awake()
    {
        theBoss = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Utilities.Pause();
            Debug.Log("PAUSE = " + isPaused);
        }
    }
}
