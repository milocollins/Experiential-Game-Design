using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Investigation Event Scriptable Object", order = 1)]
public class InvestigationEvent : ScriptableObject
{
    public bool isLocked = true;
    public int timeIndex;
    public string time;
    public Sprite locationImage;
    public string locationName;
    public string eventAbstract;
    public string participants;
    public List<EventNote> notes;

    public void Unlock()
    {
        isLocked = false;
    }
}
