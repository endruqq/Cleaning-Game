using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public int Coin;
    public TMP_Text Coin_text;

    public GameObject cleaningBottlePrefab; 
    private InventorySlot[] inventorySlots; 

    void Start()
    {
        Coin = PlayerPrefs.GetInt("Coin", 100);
        Coin = 100;
        Coin_text.text = Coin.ToString();

        
        StartCoroutine(FindInventorySlots());
    }

    IEnumerator FindInventorySlots()
    {
        yield return new WaitForSeconds(0.5f); 
        GameObject inventoryObject = GameObject.Find("Inventory"); 
        if (inventoryObject != null)
        {
            inventorySlots = inventoryObject.GetComponentsInChildren<InventorySlot>();
        }
        else
        {
            Debug.LogError("Inventory not found!");
        }
    }

    public void BuyCleaningBottle()
    {
        if (Coin >= 10)
        {
            Coin -= 10;
            Coin_text.text = Coin.ToString();
            PlayerPrefs.SetInt("Coin", Coin);
            

            AddCleaningBottleToInventory();
        }
        else
        {
            print("Not enough coins");
        }
    }

    void AddCleaningBottleToInventory()
    {
        if (cleaningBottlePrefab == null || inventorySlots == null)
        {
            Debug.LogError("Missing prefab or inventory slots!");
            return;
        }

        
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.transform.childCount == 0) 
            {
                GameObject newBottle = Instantiate(cleaningBottlePrefab, slot.transform);
                newBottle.transform.localScale = Vector3.one; 
                return;
            }
        }

        Debug.Log("No empty inventory slots available!");
    }
}
