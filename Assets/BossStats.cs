using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : MonoBehaviour
{
    public float speed;
    public float currHealth;
    public float maxHealth;
    public float damage;
    public float posType = 0;
    public float negType = 0;
    public int lightOrbs = 1;
    public GameObject LightOrb;
    public HealthBar healthBar;
    public BossHandler bossHandler;

    private void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        currHealth = maxHealth;
        LightOrb.GetComponent<LightOrbs>().lOrb = lightOrbs;
    }





    public void TakeDamage(float damage, float amp, float element)
    {
   
        if (element == posType)
        {
            currHealth -= (damage * amp);
        }
        else if (element == negType)
        {
            currHealth -= (damage/2);
        }
        else
        {
            currHealth -= damage;
        }
        healthBar.SetHealth(currHealth);
        if (currHealth <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
        Instantiate(LightOrb, transform.position, Quaternion.identity);
        bossHandler.GoHome();
    }
}
