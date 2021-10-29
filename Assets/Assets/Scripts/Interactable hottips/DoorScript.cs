using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isLocked;
    public KeyScriptableObject doorKey;
    public bool isOpen;
    private Animation myAnim;
    public AnimationClip openDoor;
    public AnimationClip closeDoor;
    public AudioClip openSFX;
    public AudioClip closeSFX;
    public AudioClip unlockSFX;
    public AudioClip lockedSFX;

    public string lockedTip;
    public string unlockTip;
    public string openTip;
    public string closeTip;
    private string currentTip;

    public Tooltip myTooltip;

    public void Awake()
    {
        myAnim = transform.GetComponent<Animation>();
        myAnim.AddClip(openDoor, "Open");
        myAnim.AddClip(closeDoor, "Close");
    }
    public void Start()
    {
        if (doorKey != null)
        {
            isLocked = true;
        }
        else
        {
            isLocked = false;
        }
        SetTooltip();
    }
    public void UseDoor()
    {
        if (!myAnim.isPlaying)
        {
            if (isLocked)
            {
                if (Inventory.theInventory.HasItem(doorKey))
                {
                    isLocked = false;
                    //SFXManager.sfxManager.PlaySFX(unlockSFX);
                    UnlockDoor();
                }
                else
                {
                    //SFXManager.sfxManager.PlaySFX(lockedSFX);
                }
            }
            else
            {
                ToggleOpen();
            }
            UIManager.ui.SetTooltip(GetTooltip());
        }
    }
    private IEnumerator PostAnimTip(float t)
    {
        yield return new WaitForSeconds(t);
        SetTooltip(true);
        UIManager.ui.SetTooltip(currentTip);
    }
    private void UnlockDoor()
    {
        //PlaySFX
        isLocked = false;
    }
    private void ToggleOpen()
    {
        if (!isOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
        IsOpenToggle();
    }
    private void OpenDoor()
    {
        //myAnim.clip = openDoor;
        myAnim.Play("Open");
        StartCoroutine(PostAnimTip(openDoor.length));
        //SFXManager.sfxManager.PlaySFX(openSFX);
    }
    private void CloseDoor()
    {
        //myAnim.clip = closeDoor;
        myAnim.Play("Close");
        StartCoroutine(PostAnimTip(closeDoor.length));
        //SFXManager.sfxManager.PlaySFX(closeSFX);
    }
    public void IsOpenToggle()
    {
        isOpen = !isOpen;
    }
    private void SetTooltip()
    {
        string s;
        if (!myAnim.isPlaying)
        {
            if (isLocked)
            {
                if (doorKey)
                {
                    if (Inventory.theInventory.HasItem(doorKey))
                    {
                        s = unlockTip;
                    }
                    else
                    {
                        s = lockedTip;
                    }
                }
                else
                {
                    s = lockedTip;
                }
            }
            else
            {
                if (isOpen)
                {
                    s = closeTip;
                }
                else
                {
                    s = openTip;
                }
            }
        }
        else
        {
            s = "";
        }
        currentTip = s;
    }
    private void SetTooltip(bool b)
    {
        string s;
        if (isLocked)
        {
            if (doorKey)
            {
                if (Inventory.theInventory.HasItem(doorKey))
                {
                    s = unlockTip;
                }
                else
                {
                    s = lockedTip;
                }
            }
            else
            {
                s = lockedTip;
            }
        }
        else
        {
            if (isOpen)
            {
                s = closeTip;
            }
            else
            {
                s = openTip;
            }
        }
        currentTip = s;
    }
    public string GetTooltip()
    {
        SetTooltip();
        return currentTip;
    }
}
