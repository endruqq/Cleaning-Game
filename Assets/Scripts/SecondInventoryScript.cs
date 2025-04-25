using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondInventoryScript : MonoBehaviour
{
    public static SecondInventoryScript Instance; 
    public GameObject[] itemPrefabs;  
    public Transform[] itemSlots;     

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

    
    public void InstantiateItemFromId(int itemId)
    {
        if (itemId >= 0 && itemId < itemPrefabs.Length)
        {
            
            GameObject item = Instantiate(itemPrefabs[itemId]);
            item.transform.SetParent(itemSlots[itemId], false); 
            item.SetActive(true); 
        }
    }
}
