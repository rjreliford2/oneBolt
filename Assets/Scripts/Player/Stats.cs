using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currHealth;
    public float lightOrbs = 0f; //money
    public float rcost = 0f;
    public int maxLives = 3;
    public int currentLives;
    public float damage = 25f;
    public float knifeDamage = 20f;
    public float speed = 5f;
    public float jumpHeight = 10f;
    public float teleportDistance = 5f;
    public float teleportDelay = 1f;
    public float iFrames = 1f;

    private HealthBar healthBar;
    private GameManager gameManager;
    
    

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
        currHealth = maxHealth;
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(lightOrbs > 1f)
        {
            rcost = Mathf.Round(lightOrbs * 0.50f);
        }
        else
        {
            rcost = 1f;
        }
        
    }

    public void TakeDamage(float damage)
    {
        currHealth -= damage;
        healthBar.SetHealth(currHealth);
        if (currHealth <= 0)
        {
            Die();
        }
        
    }

    public void Upgrade()
    {
        GetComponent<PlayerMovement>().UpdateVals();
    }

    public void Die()
    {

        lightOrbs = Mathf.RoundToInt(lightOrbs/ 2);
        gameManager.Handler(3);
    }

}
