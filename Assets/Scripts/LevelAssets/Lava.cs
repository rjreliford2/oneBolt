using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public Stats playerStats;

    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<Stats>();
    }

 

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerStats.TakeDamage(damage);
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        print("type: " + collision.gameObject.GetComponent<Bolt>().type);
        if (collision.gameObject.GetComponent<Bolt>().type == 3)
        {
            print("ice bolt hit");
            Destroy(transform.gameObject);
        }
    }
    
}
