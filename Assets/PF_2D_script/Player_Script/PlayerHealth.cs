using System.Collections;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Santé healthBar;
    public GameObject player;
    public SpriteRenderer sr;

    [SerializeField]
    private bool isInvisible;

    public float invincibilityFlashTime = 0.2f;
    public float invincibilityDurationTime = 2;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        testDamage();
        gameOver();
    }

    void testDamage()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            takeDamage(20);
        }
    }

    public void takeDamage(int damage)
    {
        if (!isInvisible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            isInvisible = true;
            StartCoroutine(invincibilityFlash());
            StartCoroutine( invincibilityDuration());
        } 
    }

    public IEnumerator invincibilityFlash()
    {
        while (isInvisible)
        {
            sr.color = new Color (1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFlashTime);
            sr.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashTime);
        }       
    }

    public IEnumerator invincibilityDuration() 
    { 
        yield return new WaitForSeconds(invincibilityDurationTime);
        isInvisible = false;
    }

    void gameOver()
    {
        if (currentHealth <= 0)
        {
            Destroy(player);
        }
    }
}
