using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    protected Collider2D col;

    [SerializeField]
    protected float offsSetTime = 0.5f;
    public void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetAxisRaw("Vertical") < 0)
        {
            StartCoroutine(changeColliderState());
        }
    }

    private IEnumerator changeColliderState()
    {
        col.enabled = false;
        yield return new WaitForSeconds(offsSetTime); ;
        col.enabled = true;
    }
}
