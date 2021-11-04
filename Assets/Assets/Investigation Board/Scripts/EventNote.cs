using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Event Note Scriptable Object", order = 1)]
public class EventNote : ScriptableObject
{
    public string text;
    public bool isLocked = true;
}
