using UnityEngine;

public class PersistentInventory : MonoBehaviour
{
    private static PersistentInventory instance;
    public GameObject inventory; 

    void Awake()
    {
        
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        
        if (inventory == null)
        {
            Debug.LogError("Inventory reference not set in PersistentInventory!");
            return;
        }

        
        inventory.SetActive(true); 
    }
}