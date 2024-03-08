using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Santé healthBar;
    public GameObject Monster;
    public SpriteRenderer sr;

    [SerializeField]
    private bool isDamage;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        Death();
        //takeDamage(20);
    }

    public void takeDamage(int damage)
    {
        if (!isDamage)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            StartCoroutine(damageFlash());
        }
        
    }
    public IEnumerator damageFlash()
    {
        while (isDamage)
        {
            sr.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(0.2f);
            sr.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    void Death()
    {
        if (currentHealth <= 0)
        {
            Destroy(Monster);
        }
    }
}
