using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobweb : MonoBehaviour
{
    public float slowSpeed;
    public GameObject player;
    private float origSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        origSpeed = player.GetComponent<Stats>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<PlayerMovement>().speed = slowSpeed;
            //print(collider.gameObject.GetComponent<Stats>().speed);
            collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxisRaw("Horizontal") * slowSpeed, collider.gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            if (collider.gameObject.GetComponent<Bolt>() != null)
            {
                if (collider.gameObject.GetComponent<Bolt>().type == 1)
                {
                    print("fire bolt hit");
                    Destroy(transform.gameObject);
                }
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<PlayerMovement>().speed = origSpeed;
            collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxisRaw("Horizontal") * origSpeed, collider.gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
