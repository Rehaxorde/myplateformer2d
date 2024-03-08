using UnityEngine;

public class redbullet : MonoBehaviour
{
    [SerializeField]
    private int damages = 20;
    [SerializeField]
    private GameObject redBullet;
    [SerializeField]
    private float vanishTime = 5;

    private void Update()
    {
        if (Time.time > vanishTime)
        {
            Destroy(redBullet);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth health = collision.transform.GetComponent<playerHealth>();
            health.takeDamage(damages);
            Destroy(redBullet);
        }
        else if (collision.CompareTag("shield"))
        {
            playerHealth health = collision.transform.GetComponent<playerHealth>();
            health.takeDamage(damages);
            Destroy(redBullet);
        }
        if (collision.CompareTag("blueShield"))
        {
            Destroy(redBullet);
        }
    }
}
