using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Character Note Scriptable Object", order = 1)]
public class CharacterNote : ScriptableObject
{
    public string text;
    public bool isLocked = true;
}
