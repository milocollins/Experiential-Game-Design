using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager ui;
    public Text tooltip;
    public bool tooltipBool = false;
    public Text interactiveSubtitle;

    [Header("Dialogue Buttons")]
    public List<GameObject> dialogueButtons = new List<GameObject>();
    private void Awake()
    {
        ui = this;
        foreach (GameObject n in dialogueButtons)
        {
            n.SetActive(false);
        }
    }
    private void Start()
    {
        tooltip.gameObject.SetActive(false);
        interactiveSubtitle.text = "";
    }
    private void Update()
    {
        if (!DialogueManager.manager.dialogueActive)
        {
            if (Input.GetButtonDown("Journal"))
            {
                Journal.theJournal.ToggleJournal();
            }
            if (Input.GetButtonDown("Map"))
            {
                Journal.theJournal.ToggleMap();
            }
        }
    }
    public void SetDialogueSubtitle(string s)
    {
        interactiveSubtitle.text = s;
    }
    public void SetTooltip(string s)
    {
        tooltipBool = true;
        tooltip.text = s;
        tooltip.gameObject.SetActive(true);
    }
    public void ResetTooltip()
    {
        tooltipBool = false;
        tooltip.gameObject.SetActive(false);
    }
}