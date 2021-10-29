using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory theInventory;
    public List<KeyScriptableObject> keyInventory;
    private void Awake()
    {
        theInventory = this;
    }
    public void Start()
    {
    }
    public void AddItem()
    {

    }
    public bool HasItem(KeyScriptableObject key)
    {
        if (key)
        {
            for (int i = 0; i < keyInventory.Count; i++)
            {
                if (keyInventory[i].name == key.name)
                {
                    return true;
                }
            }
            return false;
        }
        else
        {
            return false;
        }
    }
}
