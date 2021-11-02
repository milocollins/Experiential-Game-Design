using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Investigation : MonoBehaviour
{
    public static Investigation theInvestigation;
    private bool isOn = false;
    public Camera myCamera;
    public CharacterProfile[] suspects = new CharacterProfile[7];
    public Button[] criticalEvents = new Button[7];
    public Button[] suspectEvents = new Button[7];
    public Button[] profileButtons = new Button[7];
    public EventPoster eventPoster;
    public ProfilePoster profilePoster;
    private CharacterProfile currentProfile;
    private Button currentMotive;
    private InvestigationEvent currentEvent;

    void Awake()
    {
        theInvestigation = this;
    }
    private void Start()
    {
        
        myCamera.enabled = false;
    }
    private void Update()
    {
        if (isOn)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleInvestigation();
            }
        }
    }
    public void ToggleInvestigation()
    {
        isOn = !isOn;
        PlayerController.thePlayer.ToggleInput();
        CameraController.firstPerson.ToggleInput();
        GameManager.theBoss.InputToggle();
        myCamera.enabled = isOn;
        CameraController.firstPerson.GetComponent<Camera>().enabled = !isOn;
        Cursor.visible = isOn;
        if (isOn)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    //public void UnlockEvent(InvestigationEvent e)
    //{
    //    e.isLocked = false;
    //}
    private void GetMotives()
    {

    }
    public void SelectMotive()
    {

    }
    public void SelectSuspect()
    {

    }
    public void SelectEvent()
    {

    }
    
}
