using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public CharacterProfile myProfile;
    public InvestigationEvent myEvent;
    public Motive myMotive;
    public Image myImage;
    public Text imageText;

    public void SelectSuspect()
    {
        Debug.Log("BAM 1");
        if (myProfile != null)
        {
            Debug.Log("Suspect not null");
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
    public void SetupEvent(InvestigationEvent e)
    {
        myEvent = e;
        if (!e.isLocked)
        {
            myImage.sprite = e.locationImage;
        }
        else
        {
            myImage.sprite = Investigation.theInvestigation.dummyImage;
        }
    }
    public void ResetEvent()
    {
        myEvent = null;
        myImage.sprite = Investigation.theInvestigation.dummyImage;
    }
}
