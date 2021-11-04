using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Character Profile Scriptable Object", order = 1)]
public class CharacterProfile : ScriptableObject
{
    public int age;
    public string relation;
    public Sprite portrait;
    public InvestigationEvent[] myEvents = new InvestigationEvent[7];
    public List<Motive> myMotives;
    public List<CharacterNote> notes;
}
