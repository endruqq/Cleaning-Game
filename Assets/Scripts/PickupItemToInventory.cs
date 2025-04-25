using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItemToInventory : MonoBehaviour
{
    public float interactionRange = 0.4f;
    public LayerMask AddableItemsToInventoryLayer;
    private Inventory inventory;
    public GameObject itemPickedUp;
    public GameObject interactionIndicator;
    private Transform playerTransform;
    private bool isInRange = false;

    private void Start()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        inventory = player.GetComponent<Inventory>();

        
        interactionIndicator.gameObject.SetActive(false);
    }

    private void Update()
    {
        
        float distance = Vector2.Distance(playerTransform.position, transform.position);

        if (distance <= interactionRange)
        {
            isInRange = true;
            interactionIndicator.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                for (int i = 0; i < inventory.slots.Length; i++)
                {
                    if (inventory.isFull[i] == false)
                    {
                        inventory.isFull[i] = true;
                        Instantiate(itemPickedUp, inventory.slots[i].transform, false);
                        Destroy(gameObject);
                        break;
                    }
                }
            }
        }
        else
        {
            isInRange = false;
            interactionIndicator.gameObject.SetActive(false);
        }
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
