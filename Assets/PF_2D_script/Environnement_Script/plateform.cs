using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateform : MonoBehaviour
{
    private Collider2D col;

    [SerializeField]
    private float offsSetTime =0.5f;
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
       if (collision.gameObject.CompareTag ("Player") && Input.GetAxisRaw("Vertical") < 0)
       {
            StartCoroutine(changeColliderState());
       }
    }

    private IEnumerator changeColliderState()
    {
        col.enabled = false;
        yield return new WaitForSeconds(offsSetTime); ;
        col.enabled=true;
    }
}
