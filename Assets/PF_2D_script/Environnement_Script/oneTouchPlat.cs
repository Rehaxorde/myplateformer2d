using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oneTouchPlat : Platform
{
    [SerializeField] float fallingDelay;
    [SerializeField] float respawnDelay;
    
    private Rigidbody2D rb;
    //private Collider2D col;
    private bool iswaitingForFalling;
    private Vector2 originalPosition; 

    public new void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        base.Start();
        if (collision.gameObject.CompareTag("Player") && !iswaitingForFalling)
        {
            iswaitingForFalling = true;
            Invoke("fall", fallingDelay);
        }
    }

    void fall()
    {
        col.enabled = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        Invoke("respawn", respawnDelay);
    }
    void respawn() 
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.rotation = Quaternion.identity;
        transform.position = originalPosition;
        rb.bodyType = RigidbodyType2D.Static;
        col.enabled = true;
        iswaitingForFalling = false;
    }
}
