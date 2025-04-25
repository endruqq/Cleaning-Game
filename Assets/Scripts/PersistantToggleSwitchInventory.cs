using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantToggleSwitchInventory : MonoBehaviour
{

    private static PersistantToggleSwitchInventory instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}
