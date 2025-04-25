using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (inventoryItem != null)
        {
            HandleItemDrop(inventoryItem);
        }
    }

    public void HandleItemDrop(InventoryItem item)
    {
        item.parentAfterDrag = transform;
    }


}