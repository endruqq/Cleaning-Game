using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotForMIrror : MonoBehaviour
{
    public int slotId;  
    public InventoryItem currentItem;  

    public void SetItem(InventoryItem item)
    {
        currentItem = item;
    }

    public void ClearSlot()
    {
        currentItem = null;
    }
}
