using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOfPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
