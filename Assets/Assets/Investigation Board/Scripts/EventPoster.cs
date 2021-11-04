using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventPoster : MonoBehaviour
{
    public Image locationImage;
    public Text imageTitle;
    public Text myLocation;
    public Text participants;
    public Text time;
    public Text eventAbstract;
    public List<Text> notes;
    private void Start()
    {
        ResetPoster();
    }
    public void ResetPoster()
    {
        locationImage.sprite = Investigation.theInvestigation.dummyImage;
        myLocation.text = "Location: ";
        participants.text = "Participants: ";
        time.text = "Event Time: ";
        eventAbstract.text = "Abstract: ";
        imageTitle.text = "?";
        for (int i = 0; i < notes.Count; i++)
        {
            notes[i].text = null;
        }
    }
    public void SetupPoster(InvestigationEvent e)
    {
        if (!e.isLocked)
        {
            imageTitle.text = "";
            locationImage.sprite = e.locationImage;
            myLocation.text = "Location: " + e.locationName;
            participants.text = "Participants: " + e.participants;
            time.text = "Event Time: " + e.time;
            eventAbstract.text = "Abstract: " + e.eventAbstract;
            int j = 0;
            for (int i = 0; i < e.notes.Count; i++)
            {
                if (e.notes[i])
                {
                    if (!e.notes[i].isLocked)
                    {
                        notes[j].text = e.notes[i].text;
                        j++;
                    }
                }
            }
        }
        else
        {
            ResetPoster();
        }
    }
    //ADD THE MOTIVES AND NOTES TITLES
}
