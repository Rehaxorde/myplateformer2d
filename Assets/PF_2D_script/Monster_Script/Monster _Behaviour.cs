using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Behaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnColliderEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerHealth health = collision.transform.GetComponent<playerHealth>();
            health.takeDamage(20);
        }
    }
}
