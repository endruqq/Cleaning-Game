using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotForToolBar : MonoBehaviour, IDropHandler
{
    public int slotId;
    public InventoryItem currentItem;

    public static Dictionary<int, InventorySlotForToolBar> slots = new Dictionary<int, InventorySlotForToolBar>();

    // Map inventory slot IDs to corresponding mirrored slot IDs (updated to 0->1, 2->3)
    private Dictionary<int, int> slotMapping = new Dictionary<int, int>
    {
        { 0, 1 },  // Inventory slot 0 -> mirrored slot 1
        { 2, 3 },  // Inventory slot 2 -> mirrored slot 3
        // You can add more mappings if necessary
    };

    private void Awake()
    {
        // Register the slot in the global dictionary based on its slot ID
        if (!slots.ContainsKey(slotId))
        {
            slots[slotId] = this;
        }
        else
        {
            Debug.LogWarning($"Slot ID {slotId} already exists!");
        }
    }

    // Handle the drop event (item dropped into the slot)
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            Debug.Log(inventoryItem != null ? "Item found" : "No item found");
            if (inventoryItem != null)
            {
                inventoryItem.parentAfterDrag = transform;
                currentItem = inventoryItem;

                // Only update mirrored items if the slot ID is in the mapping
                if (slotMapping.ContainsKey(slotId))
                {
                    UpdateMirroredItems();
                }
            }
        }
    }

    // Called when the children of the transform change (e.g., when items are removed)
    private void OnTransformChildrenChanged()
    {
        if (transform.childCount == 0 && slotMapping.ContainsKey(slotId))
        {
            DeactivateMirroredItem();
            currentItem = null;
        }
    }

    // Update mirrored item based on the mapping
    void UpdateMirroredItems()
    {
        AllItemsForInventory allItems = FindFirstObjectByType<AllItemsForInventory>();
        if (allItems == null || currentItem == null) return;

        // Find the corresponding mirrored slot ID based on the map
        if (slotMapping.ContainsKey(slotId))
        {
            int mirroredSlotId = slotMapping[slotId];

            // Get the mirrored item based on the current item ID
            GameObject mirroredItem = allItems.GetMirroredItemById(currentItem.id);
            if (mirroredItem != null)
            {
                mirroredItem.SetActive(true);
                mirroredItem.transform.SetParent(slots[mirroredSlotId].transform);  // Use the new mirrored slot ID
                mirroredItem.transform.localPosition = Vector3.zero;

                // Update the mirrored slot's current item
                InventorySlotForToolBar mirroredSlot = slots[mirroredSlotId];
                if (mirroredSlot != null)
                {
                    mirroredSlot.currentItem = currentItem;
                }
            }
        }
    }

    // Deactivate mirrored item when item is removed from the slot
    void DeactivateMirroredItem()
    {
        AllItemsForInventory allItems = FindFirstObjectByType<AllItemsForInventory>();
        if (allItems == null || currentItem == null) return;

        // Get the mirrored item by ID and deactivate it
        GameObject mirroredItem = allItems.GetMirroredItemById(currentItem.id);
        if (mirroredItem != null)
        {
            mirroredItem.SetActive(false);

            // Remove the item from the mirrored slot
            InventorySlotForToolBar mirroredSlot = slots[slotId];
            if (mirroredSlot != null)
            {
                mirroredSlot.currentItem = null;
            }
        }
    }
}
