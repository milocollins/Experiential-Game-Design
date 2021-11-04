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
    public GameObject currentSuspect;
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
        //Reset suspect, motives, posters and events
        foreach (Button b in suspectEvents)
        {
            b.GetComponent<ButtonHandler>().ResetEvent();
        }
        currentSuspect.transform.GetChild(0).GetComponent<Image>().sprite = dummyImage;
        currentSuspect.transform.GetChild(1).GetComponent<Text>().text = "?";
        profilePoster.ResetPoster();
        eventPoster.ResetPoster();
        ResetMotives();
    }
    private void ResetMotives()
    {
        for (int i = 0; i < motiveGOs.Length; i++)
        {
            motiveGOs[i].transform.GetChild(0).GetComponent<Text>().text = null;
            motiveGOs[i].transform.GetChild(1).GetComponent<Text>().text = null;
            motiveGOs[i].SetActive(false);
        }
    }
    private void GetMotives(CharacterProfile c)
    {
        ResetMotives();
        for (int i = 0; i < motiveGOs.Length; i++)
        {
            if (c.myMotives[i] && !c.myMotives[i].isLocked)
            {
                motiveGOs[i].SetActive(true);
                motiveGOs[i].transform.GetChild(0).GetComponent<Text>().text = c.myMotives[i].title;
                motiveGOs[i].transform.GetChild(1).GetComponent<Text>().text = c.myMotives[i].description;
            }
        }
    }
    public void SelectSuspect(CharacterProfile c)
    {
        profilePoster.SetupPoster(c);
        currentSuspect.transform.GetChild(0).GetComponent<Image>().sprite = c.portrait;
        currentSuspect.transform.GetChild(1).GetComponent<Text>().text = c.name;
        for (int i = 0; i < suspectEvents.Length; i++)
        {
            if (c.myEvents[i] != null)
            {
                GetMotives(c);
                Debug.Log("EVENT NOT EMPTY");
                if (!c.myEvents[i].isLocked)
                {
                    suspectEvents[i].GetComponent<ButtonHandler>().SetupEvent(c.myEvents[i]);
                }
                else
                {
                    suspectEvents[i].GetComponent<ButtonHandler>().ResetEvent();
                }
            }
            else
            {
                Debug.Log("NULL ERROR");
            }
        }
    }
    public void SelectEvent(InvestigationEvent e)
    {
        eventPoster.SetupPoster(e);
    }
    
}
