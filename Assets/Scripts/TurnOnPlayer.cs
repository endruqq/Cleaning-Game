using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TurnOnPlayer : MonoBehaviour
{
    void Start()
    {
        // Only activate the Player root GameObject — not its children individually
        GameObject player = FindInactiveObjectByTag("Player");
        if (player != null)
        {
            player.SetActive(true);
        }

        // Recursively activate all children under InventorySlotTool
        GameObject inventorySlotTools = FindInactiveObjectByTag("InventorySlotTool");
        if (inventorySlotTools != null)
        {
            ActivateAllChildren(inventorySlotTools);
        }

    }

    void ActivateAllChildren(GameObject obj)
    {
        obj.SetActive(true);

        foreach (Transform child in obj.transform)
        {
            ActivateAllChildren(child.gameObject);
        }
    }

    GameObject FindInactiveObjectByTag(string tag)
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag(tag) && !obj.activeInHierarchy)
            {
                return obj;
            }
        }

        return null;
    }
}
