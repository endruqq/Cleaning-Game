using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chimney : MonoBehaviour
{
    private bool isBurning = false;
    public GameObject fire;
    public GameObject glassPrefab; // Assign your glass prefab in Inspector
    public Transform outputPosition; // Where the glass will spawn
    public float productionTime = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Burnable") && !isBurning)
        {
            // Standard fuel item
            Destroy(other.gameObject);
            StartBurning();
        }
        else if (isBurning && other.CompareTag("Sand"))
        {
            // Sand processing
            Destroy(other.gameObject);
            StartCoroutine(ProduceGlass());
        }
    }

    void StartBurning()
    {
        isBurning = true;
        fire.SetActive(true);
        Debug.Log("Burning started!");
    }

    IEnumerator ProduceGlass()
    {
        Debug.Log("Processing sand into glass...");
        yield return new WaitForSeconds(productionTime);

        if (glassPrefab != null && outputPosition != null)
        {
            GameObject newGlass = Instantiate(glassPrefab, outputPosition.position, Quaternion.identity);
            newGlass.tag = "Glass"; // Optional: set the tag
            Debug.Log("Glass produced!");
        }
    }
}
