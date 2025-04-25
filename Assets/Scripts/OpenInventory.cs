using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenInventory : MonoBehaviour
{
    public GameObject inventoryCanvas;
    public bool isActive;
    void Start()
    {
        
        SceneManager.sceneLoaded += OnSceneLoaded; 
        inventoryCanvas.SetActive(false);
        isActive = false;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (inventoryCanvas == null)
        {
            inventoryCanvas = GameObject.Find("CanvasForInventory"); 
        }
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)) {
            isActive = !isActive;
            inventoryCanvas.SetActive(isActive);
        }
  
    }
}
