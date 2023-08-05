using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float speed;
    public float maxHealth;
    public float currHealth;
    public float damage = 20;
    public float posType = 0;
    public float negType = 0;
    public int lightOrbs = 20;
    public GameObject LightOrb;
    public HealthBar healthBar;
    public GameObject deathObject;
    public AudioClip deathSound;
    public AudioClip hitSound;
    private AudioSource mouth;


    private void Start()
    {
        mouth = GetComponent<AudioSource>();
        mouth.clip = hitSound;
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
        currHealth = maxHealth;
        LightOrb.GetComponent<LightOrbs>().lOrb = lightOrbs;
        
    }


    private  void OnCollisionEnter2D( Collision2D collision){
        if (collision.gameObject.TryGetComponent<Stats>(out Stats playerComponent))
        {
            playerComponent.TakeDamage(damage);

        }
    }

    public void TakeDamage(float damage, float amp, float element, float knockBack, Vector3 attacker)
    {
        bool pos = false;
        if (element == posType)
        {
            currHealth -= damage * amp;
            pos = true;
        }
        else if(element == negType)
        {
            currHealth -=  damage/2  ;
        }
        else
        {
            currHealth -= damage;
            pos = true;
        }

        if (GetComponent<Rigidbody2D>() != null && pos)
        {
            Vector3 knockbackDirection = (transform.position - attacker).normalized;
            Vector3 knockback = knockbackDirection * knockBack;
            knockback.y = 2f;
            GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);

        }
        
        healthBar.SetHealth(currHealth);
        if(currHealth <= 0)
        {
            Die();
        }
        
    }

    public void Die()
    {
        Instantiate(LightOrb, transform.position, Quaternion.identity);
        GameObject sound = Instantiate(deathObject, transform.position, Quaternion.identity);
        sound.GetComponent<AudioSource>().clip = deathSound;
        sound.GetComponent<AudioSource>().Play();
        Destroy(sound, 4f);
        Destroy(gameObject);


    }
}
