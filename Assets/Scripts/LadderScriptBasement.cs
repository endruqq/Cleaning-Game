using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScriptBasement : MonoBehaviour
{
    private GameObject player;
    public GameObject houseLadder;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.transform.position = houseLadder.transform.position + new Vector3(1, 0, 0);
    }

}
