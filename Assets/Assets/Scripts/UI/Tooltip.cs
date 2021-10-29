using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public string tipText;
    public void SetActive()
    {
        if (transform.GetComponentInParent<DoorScript>())
        {
            tipText = transform.GetComponentInParent<DoorScript>().GetTooltip();
        }
        UIManager.ui.SetTooltip(tipText);
    }
}
