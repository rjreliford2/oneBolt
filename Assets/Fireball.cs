using System;
using UnityEngine;
using UnityEngine.Android;

public class Fireball : MonoBehaviour
{

    public float damage;
    public float speed;


    private void Start()
    {

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = rb.velocity.normalized * speed;
        Destroy(gameObject, 6f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Stats>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}