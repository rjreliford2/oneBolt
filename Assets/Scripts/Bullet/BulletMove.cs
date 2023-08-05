using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float BulletSpeed = 8f;
    private float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.right * Time.deltaTime * BulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.TryGetComponent<Stats>(out Stats playerComponent))
        {
            playerComponent.TakeDamage(damage);

        } if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player")
        { Destroy(gameObject);
        }
       
    }
    public void setDamage(float damage){
        this.damage = damage;
    }
    
}
