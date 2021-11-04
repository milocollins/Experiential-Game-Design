using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePoster : MonoBehaviour
{
    public Image portrait;
    public Text portraitText;
    public Text suspectName;
    public Text relation;
    public Text age;
    public List<Text> motives;
    public List<Text> notes;
    private void Start()
    {
        ResetPoster();
    }
    public void ResetPoster()
    {
        portrait.sprite = Investigation.theInvestigation.dummyImage;
        suspectName.text = "Suspect Name: ";
        relation.text = "Relation: ";
        age.text = "Age: ";
        portraitText.text = "?";
        for (int i = 0; i < notes.Count; i++)
        {
            notes[i].text = null;
        }
        for (int i = 0; i < motives.Count; i++)
        {
            motives[i].text = null;
        }
    }
    public void SetupPoster(CharacterProfile c)
    {
        Debug.Log("Made it");
        portraitText.text = c.name;
        portrait.sprite = c.portrait;
        suspectName.text = "Suspect Name: " + c.name;
        relation.text = "Relation: " + c.relation;
        age.text = "Age: " + c.age;
        int j = 0;
        for (int i = 0; i < c.notes.Count; i++)
        {
            if (c.notes[i])
            {
                if (!c.notes[i].isLocked)
                {
                    notes[j].text = c.notes[i].text;
                    j++;
                }
            }
        }
        j = 0;
        for (int i = 0; i < c.myMotives.Count; i++)
        {
            if (c.myMotives[i])
            {
                if (!c.myMotives[i].isLocked)
                {
                    motives[j].text = c.myMotives[i].description;
                    j++;
                }
            }
        }
    }
    //ADD THE NOTES TITLES
}
