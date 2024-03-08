using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMobile : Platform
{
   protected Dictionary<Transform,Transform> family;

    public new void Start()
    {
        base.Start();
        family = new Dictionary<Transform,Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TestTag(collision.gameObject))
        {
            family.Add(collision.gameObject.transform,collision.gameObject.transform.parent);
            collision.gameObject.transform.SetParent(transform, true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (TestTag(collision.gameObject))
        {
            Transform formerParent;
            family.TryGetValue(collision.gameObject.transform, out formerParent);           
            collision.gameObject.transform.SetParent(formerParent, true);
            family.Remove(collision.gameObject.transform);          
        }
    }
    private bool TestTag(GameObject go)
    {
        return (go.CompareTag("Player") || go.CompareTag("enemy"));
    }
}
