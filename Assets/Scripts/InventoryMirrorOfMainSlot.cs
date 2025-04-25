using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryMirrorOfMainSlot : MonoBehaviour
{
    public Transform gridParent; 

    public void MirrorItem()
    {
        if (transform.childCount == 0) return;

        GameObject originalItem = transform.GetChild(0).gameObject;

        
        GameObject copiedItem = Instantiate(originalItem, gridParent);
        copiedItem.transform.localScale = Vector3.one;

        
        LayoutRebuilder.ForceRebuildLayoutImmediate(gridParent.GetComponent<RectTransform>());
    }
}
