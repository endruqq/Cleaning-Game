using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Required for button and UI components

public class TonnyShop : MonoBehaviour
{
    // Existing variables
    private InventorySlot[] playerInventorySlots;
    private InventorySlot[] sellerInventorySlots;
    private Transform persistentInventory;

    // New variables for selling system
    [Header("Selling System")]
    public Button sellButton;
    public TMP_Text currencyText; // UI text to display currency
    private static int playerCurrency;

    void Start()
    {
        StartCoroutine(FindInventory());

        // Setup sell button click listener
        if (sellButton != null)
        {
            sellButton.onClick.AddListener(SellItemsInSellerInventory);
        }
        else
        {
            Debug.LogError("Sell button reference missing!");
        }

        UpdateCurrencyDisplay();
    }

    // Add this new method to handle selling
    public void SellItemsInSellerInventory()
    {
        int itemsSold = 0;

        foreach (InventorySlot slot in sellerInventorySlots)
        {
            // Check if slot has an item
            if (slot.transform.childCount > 0)
            {
                // Destroy the item and count it
                Destroy(slot.transform.GetChild(0).gameObject);
                itemsSold++;
            }
        }

        // Add currency (1 per item)
        playerCurrency += itemsSold;
        UpdateCurrencyDisplay();

        Debug.Log($"Sold {itemsSold} items. Total currency: {playerCurrency}");
    }

    void UpdateCurrencyDisplay()
    {
        if (currencyText != null)
        {
            currencyText.text = $"{playerCurrency}";
        }
    }

    IEnumerator FindInventory()
    {
        yield return new WaitForSeconds(0.5f);

        // Existing player inventory setup...

        // Find Seller Inventory
        GameObject sellerInventoryObj = GameObject.Find("SellerInventory");
        if (sellerInventoryObj != null)
        {
            sellerInventorySlots = sellerInventoryObj.GetComponentsInChildren<InventorySlot>(true);
        }
        else
        {
            Debug.LogError("SellerInventory not found!");
        }

        yield return new WaitForSeconds(0.5f);

        // Find current scene's inventory slots
        GameObject playerInventoryObj = GameObject.Find("PlayerInventory");
        if (playerInventoryObj != null)
        {
            playerInventorySlots = playerInventoryObj.GetComponentsInChildren<InventorySlot>(true);
        }
        else
        {
            Debug.LogError("PlayerInventory not found!");
            yield break;
        }

        // Find persistent inventory even if inactive
        persistentInventory = FindInactiveObject("Inventory");
        if (persistentInventory == null)
        {
            Debug.LogError("Persistent inventory not found!");
            yield break;
        }

        // Hide persistent inventory but keep items
        persistentInventory.gameObject.SetActive(false);
        LoadItemsIntoScene();
    }

    Transform FindInactiveObject(string objectName)
    {
        foreach (Transform obj in Resources.FindObjectsOfTypeAll<Transform>())
        {
            if (obj.name == objectName && obj.gameObject.scene.isLoaded)
            {
                return obj;
            }
        }
        return null;
    }

    void LoadItemsIntoScene()
    {
        int slotIndex = 0;

        // First get all slots from persistent inventory
        InventorySlot[] persistentSlots = persistentInventory.GetComponentsInChildren<InventorySlot>(true);

        foreach (InventorySlot pSlot in persistentSlots)
        {
            // Skip empty slots
            if (pSlot.transform.childCount == 0) continue;

            // Get the actual item from persistent slot
            Transform item = pSlot.transform.GetChild(0);

            // Move it to the new slot
            if (slotIndex < playerInventorySlots.Length)
            {
                item.SetParent(playerInventorySlots[slotIndex].transform);
                item.localPosition = Vector3.zero;
                item.localScale = Vector3.one;
                item.gameObject.SetActive(true);
                slotIndex++;
            }
        }

        persistentInventory.gameObject.SetActive(false);
    }
}