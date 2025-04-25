using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItem : MonoBehaviour
{
    public Transform holdSpot;
    public LayerMask pickUpMask;
    public LayerMask wallMask; // Add this to detect walls
    public Vector3 Direction { get; set; }
    private GameObject itemHolding;
    public float throwDistance = 2f; // Configurable throw distance

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (itemHolding)
            {
                // Drop item with wall check
                Vector3 dropPosition = transform.position + Direction;

                // Check if there's a wall in the way
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Direction, Direction.magnitude, wallMask);
                if (hit.collider != null)
                {
                    // If wall detected, drop just before the wall
                    Vector3 adjustedDirection = Direction.normalized * 0.1f;
                    dropPosition = new Vector3(hit.point.x, hit.point.y, transform.position.z) - adjustedDirection;
                }

                itemHolding.transform.position = dropPosition;
                itemHolding.transform.parent = null;
                if (itemHolding.GetComponent<Rigidbody2D>())
                    itemHolding.GetComponent<Rigidbody2D>().simulated = true;
                itemHolding = null;
            }
            else
            {
                Collider2D grabItem = Physics2D.OverlapCircle(transform.position + Direction, .4f, pickUpMask);
                if (grabItem)
                {
                    itemHolding = grabItem.gameObject;
                    itemHolding.transform.position = holdSpot.position;
                    itemHolding.transform.parent = transform;
                    if (itemHolding.GetComponent<Rigidbody2D>())
                        itemHolding.GetComponent<Rigidbody2D>().simulated = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (itemHolding)
            {
                StartCoroutine(ThrowItem(itemHolding));
                itemHolding = null;
            }
        }
    }

    IEnumerator ThrowItem(GameObject item)
    {
        Vector3 startPoint = item.transform.position;

        // Calculate end point with wall collision check
        Vector3 targetEndPoint = transform.position + Direction * throwDistance;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Direction, throwDistance, wallMask);
        Vector3 endPoint;

        if (hit.collider != null)
        {
            // If there's a wall, throw to just before the wall
            Vector3 adjustedDirection = Direction.normalized * 0.1f;
            endPoint = new Vector3(hit.point.x, hit.point.y, transform.position.z) - adjustedDirection;
        }
        else
        {
            endPoint = targetEndPoint;
        }

        item.transform.parent = null;

        for (int i = 0; i < 25; i++)
        {
            item.transform.position = Vector3.Lerp(startPoint, endPoint, i * .04f);
            yield return null;
        }

        if (item.GetComponent<Rigidbody2D>())
            item.GetComponent<Rigidbody2D>().simulated = true;
    }
}