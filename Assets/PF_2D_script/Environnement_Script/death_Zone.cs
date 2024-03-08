using UnityEngine;

public class death_Zone : MonoBehaviour
{
    public int damages = 20;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.transform.position = GameObject.FindGameObjectWithTag("playerSpawn").transform.position;
            playerHealth health = collision.transform.GetComponent<playerHealth>();
            health.takeDamage(damages);

        }
    }
}
