using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField]
    private int damages = 20;
    [SerializeField]
    private GameObject blueBullet;
    [SerializeField]
    private GameObject greenBullet;
    [SerializeField]
    private GameObject redBullet;
    [SerializeField]
    private float vanishTime = 5;

    public player_behavior playerBehavior;
    public PlayerMagic playerMagic;

    private void Update()
    {
        if (Time.time > vanishTime)
        {
            Destroy(blueBullet);
            Destroy(greenBullet);
            Destroy(redBullet);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Tire bleu
        if (collision.CompareTag("Player"))
        {
            playerHealth health = collision.transform.GetComponent<playerHealth>();
            health.takeDamage(damages);
            Destroy(blueBullet);
        }
        else if (collision.CompareTag("shield") && playerBehavior.blue)
        {
            if(playerMagic.GetMagicGain() < 5)
            {
                playerMagic.MagicGain(1);
            }
            Destroy(blueBullet);
        }

        //Tir vert
        if (collision.CompareTag("Player"))
        {
            playerHealth health = collision.transform.GetComponent<playerHealth>();
            health.takeDamage(damages);
            Destroy(greenBullet);
        }
        else if (collision.CompareTag("shield") && playerBehavior.green)
        {
            if (playerMagic.GetMagicGain() < 5)
            {
                playerMagic.MagicGain(1);
            }
            Destroy(greenBullet);
        }

        //Tir rouge
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
