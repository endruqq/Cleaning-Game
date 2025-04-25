using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllItemsForInventory : MonoBehaviour
{
    public GameObject cleaningAssets;
    public GameObject brush;
    public GameObject duck;
    public GameObject bleach;
    public GameObject RedBottle;
    public Transform position;

    private Dictionary<int, GameObject> mirroredItemsById = new Dictionary<int, GameObject>();

    private void Start()
    {
        AddMirroredItem(brush, 1);
        AddMirroredItem(cleaningAssets, 2);
        AddMirroredItem(duck, 3);
        
        AddMirroredItem(RedBottle, 8);
        AddMirroredItem(bleach, 9);
    }

    void AddMirroredItem(GameObject prefab, int id)
    {
        GameObject instance = Instantiate(prefab, position);
        instance.SetActive(false);
        mirroredItemsById[id] = instance;
    }

    public GameObject GetMirroredItemById(int id)
    {
        return mirroredItemsById.ContainsKey(id) ? mirroredItemsById[id] : null;
    }
}