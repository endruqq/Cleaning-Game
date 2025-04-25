using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstInventoryScript : MonoBehaviour
{

    public static FirstInventoryScript Instance;  
    public InventorySlot[] slots;  

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    
    public void TransferItemToSecondInventory(int slotId, int itemId)
    {
       
        
    }

}
