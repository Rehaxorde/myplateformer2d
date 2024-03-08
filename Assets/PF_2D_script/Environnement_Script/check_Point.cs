using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check_Point : MonoBehaviour
{
    public Transform playerSpawn;
    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("playerSpawn").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerSpawn.position = transform.position;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        
    }

}
