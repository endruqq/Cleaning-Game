using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class persistantToolInventory : MonoBehaviour
{
    public GameObject inventoryToPersist; // Assign your inventory GameObject in Inspector

    private static persistantToolInventory instance;

    private void Awake()
    {
        // Singleton pattern to prevent duplicates
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Safety checks
        if (inventoryToPersist == null)
        {
            Debug.LogError("No inventory assigned to persist! Please assign in Inspector.");
            return;
        }

        // Make sure the inventory stays active
        inventoryToPersist.SetActive(true);

        // Make the inventory a child of this persistent object if it isn't already
        if (inventoryToPersist.transform.parent != transform)
        {
            inventoryToPersist.transform.SetParent(transform);
        }
    }
}
