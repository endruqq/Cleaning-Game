using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScriptHouse : MonoBehaviour
{
    private GameObject player;
    public GameObject basementLadder;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.transform.position = basementLadder.transform.position + new Vector3(0, -1, 0);
    }

}
