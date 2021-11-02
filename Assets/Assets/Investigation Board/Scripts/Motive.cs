using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Motive Scriptable Object", order = 1)]
public class Motive : ScriptableObject
{
    public bool isLocked = true;
    public string description;
    public string title;

    public void Unlock()
    {
        isLocked = false;
    }
}
