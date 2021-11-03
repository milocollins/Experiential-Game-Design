using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public CharacterProfile myProfile;
    public InvestigationEvent myEvent;
    public Motive myMotive;

    public void SelectMotive()
    {
        if (myMotive!= null)
        {
            Investigation.theInvestigation.SelectMotive(myMotive);
        }
    }
    public void SelectSuspect()
    {
        if (myProfile != null)
        {
            Investigation.theInvestigation.SelectSuspect(myProfile);
        }
    }
    public void SelectEvent()
    {
        if (myEvent != null)
        {
            Investigation.theInvestigation.SelectEvent(myEvent);
        }
    }
}
