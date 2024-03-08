using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinkPlatform : Platform
{
    private SpriteRenderer sr;

    private Color originalColor;
    private Color ghostColor;
    private bool isSolide;

    [SerializeField]
    private float ghostAlpha = 0.5f;

    [SerializeField]
    private float switchInterval = 3f;

    public new void Start()
    {
        base.Start();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        ghostColor = sr.color;
        ghostColor.a = ghostAlpha;
        Invoke("beSolise", switchInterval);
    }

    void beSolise() 
    { 
        col.enabled = true;
        sr.color = originalColor;
        Invoke("beGhost", switchInterval);
    }

    void beGhost() 
    { 
        col.enabled = false;
        sr.color = ghostColor;
        Invoke("beSolise", switchInterval);
    }
}
