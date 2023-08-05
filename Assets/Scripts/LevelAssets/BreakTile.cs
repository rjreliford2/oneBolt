using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        //if (other.CompareTag("Player")) // will be bolt variable
        //{
        //if (other.gameObject.name == "Bolt(Clone)")
        //{
            Destroy(gameObject);
        //}
        //}
    }
}
