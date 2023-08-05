using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToChain : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.gameObject.GetComponent<DistanceJoint2D>().enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print("stick to chain");
        //player.GetComponent<Rigidbody2D>().useGravity = false;
        //collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        //collision.gameObject.GetComponent<Rigidbody2D>().constraints &= RigidbodyConstraints2D.FreezeRotation;
        transform.gameObject.GetComponent<DistanceJoint2D>().enabled = true;
        transform.gameObject.GetComponent<DistanceJoint2D>().distance = 2.5f;
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        print("stick to chain");
        //player.GetComponent<Rigidbody2D>().useGravity = false;
        //collision.gameObject.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        //collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
