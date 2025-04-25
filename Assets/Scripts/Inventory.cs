using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull; 
    public GameObject[] slots; 
    public HandSlotDisplay handSlotDisplay; 

    private void Start()
    {
        
        isFull = new bool[slots.Length];

        
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount > 0)
            {
                isFull[i] = true;
            }
        }
    }

    
    public void OnItemPlacedInHandSlot(InventoryItem item)
    {
        
    }

}