using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Investigation Event Scriptable Object", order = 1)]
public class InvestigationEvent : ScriptableObject
{
    public bool isLocked = true;
    public int timeIndex;
    public Sprite locationImage;
    public string locationName;
    public string eventAbstract;
    public List<string> participants;
    public List<string> notes;

    public void Unlock()
    {
        isLocked = false;
    }
}
