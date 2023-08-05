using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        print("type: " + collision.gameObject.GetComponent<Bolt>().type);
        if (collision.gameObject.GetComponent<Bolt>().type == 2)
        {
            print("earth bolt hit");
            Destroy(transform.gameObject);
        }
    }
}
