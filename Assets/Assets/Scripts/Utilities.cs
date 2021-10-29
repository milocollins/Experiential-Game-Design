using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities 
{
    //REFERENCE INVENTORY SCRIPT?
    public static void HasItem()
    {

    }
    public static void Interact(Transform t)
    {
        switch (t.tag)
        {
            case "Door":
                t.GetComponentInParent<DoorScript>().UseDoor();
                break;
            case "Npc":
                DialogueManager.manager.StartDialogue(t);
                break;
            default:
                break;
        }
    }
    public static void Pause()
    {
        if (GameManager.theBoss.isPaused)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
        GameManager.theBoss.isPaused = !GameManager.theBoss.isPaused;
    }
    public static void QuitGame()
    {
        Application.Quit();
    }
}
