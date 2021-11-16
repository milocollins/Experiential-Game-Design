using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueNode : BaseNode {

	[Input] public int entry;
	[Output] public int exit;

    public CharacterProfile myCharacter;
	public string dialogueText;
	public Motive myMotive;
	public InvestigationEvent myIE;
	public EventNote myEN;
	public CharacterNote myCN;
	public CharacterHandler.Emotion myEmotion;
	public bool isLocked;

	public override string ReturnString()
    {
        return myCharacter.name + "/" + dialogueText;
    }
    public override bool GetBool()
    {
        return isLocked;
    }
	public override void UnlockInformation()
    {
        if (myMotive)
        {
			myMotive.Unlock();
        }
        if (myIE)
        {
			myIE.Unlock();
        }
        if (myEN)
        {
            myEN.Unlock();
        }
        if (myCN)
        {
            myCN.Unlock();
        }

    }
}