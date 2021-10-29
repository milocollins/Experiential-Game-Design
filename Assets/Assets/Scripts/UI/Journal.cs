using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour
{
    public static Journal theJournal;
    public enum Page
    {
        Home,
        Settings,
        Map,
        Investigation
    }
    public Page currentPage = Page.Home;
    Dictionary<Page, GameObject> pageDictionary = new Dictionary<Page, GameObject>();
    public bool journalOpen = false;
    internal GameObject journalObj;
    public GameObject homePage;
    public GameObject settingsPage;
    public GameObject mapPage;
    public GameObject investigationPage;
    public AudioClip openSFX;
    public AudioClip turnPageSFX;
    public AudioClip closeSFX;
    //BUTTONS AND PAGE MANAGER FOR THE JOURNAL 
    private void Awake()
    {
        theJournal = this;
        journalObj = transform.GetChild(0).gameObject;
        journalObj.SetActive(false);
        settingsPage.SetActive(false);
        pageDictionary.Add(Page.Home, homePage);
        pageDictionary.Add(Page.Settings, settingsPage);
        pageDictionary.Add(Page.Map, mapPage);
        pageDictionary.Add(Page.Investigation, investigationPage);
    }
    internal void ToggleJournal()
    {
        if (!journalOpen)
        {
            OpenJournal(Page.Home);
        }
        else if(currentPage == Page.Home)
        {
            CloseJournal();
        }
        else
        {
            ChangePage(Page.Home);
        }
    }
    internal void ToggleJournal(Page p)
    {
        if (!journalOpen)
        {
            OpenJournal(p);
        }
        else 
        {
            CloseJournal();
        }
    }
    internal void ToggleMap()
    {
        if (currentPage!= Page.Map && journalOpen)
        {
            ChangePage(Page.Map);
        }
        else
        {
            ToggleJournal(Page.Map);
        }
    }
    public void ChangePage(Page p)
    {
        currentPage = p;
        foreach (var key in pageDictionary.Keys)
        {
            if (key != p)
            {
                pageDictionary[key].SetActive(false);
            }
            else
            {
                pageDictionary[key].SetActive(true);
            }
        }
        if (journalOpen)
        {
            SFXManager.sfxManager.PlaySFX(turnPageSFX);
        }
    }
    public void OpenJournal(Page p)
    {
        PlayerController.thePlayer.ToggleInput();
        CameraController.firstPerson.ToggleInput();
        UIManager.ui.ResetTooltip();
        ChangePage(p);
        journalOpen = true;
        journalObj.SetActive(true);
        SFXManager.sfxManager.PlaySFX(openSFX);
    }
    public void CloseJournal()
    {
        PlayerController.thePlayer.ToggleInput();
        CameraController.firstPerson.ToggleInput();
        journalOpen = false;
        journalObj.SetActive(false);
        SFXManager.sfxManager.PlaySFX(closeSFX);
    }
    // PUBLIC HOME PAGE
    // PUBLIC SETTINGS
    // CHANGE TO PAGE

}
