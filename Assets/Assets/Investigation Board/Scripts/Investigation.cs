using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Investigation : MonoBehaviour
{
    public static Investigation theInvestigation;
    private bool isOn = false;
    public Camera myCamera;
    public Button[] criticalEvents = new Button[7];
    public Button[] suspectEvents = new Button[7];
    public Button[] profileButtons = new Button[7];
    public GameObject[] motiveGOs = new GameObject[3];
    public EventPoster eventPoster;
    public ProfilePoster profilePoster;
    private CharacterProfile currentProfile;
    private Button currentMotive;
    private InvestigationEvent currentEvent;
    public Sprite dummyImage;

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
    private void GetMotives()
    {

    }
    public void SelectMotive(Motive m)
    {

    }
    public void SelectSuspect(CharacterProfile c)
    {
        
    }
    public void SelectEvent(InvestigationEvent e)
    {

    }
    
}
