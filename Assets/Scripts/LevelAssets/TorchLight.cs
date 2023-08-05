using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchLight : MonoBehaviour
{
    public GameObject torchLit;
    public GameObject torchUnlit;
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
        if (collision.gameObject.GetComponent<Bolt>().type == 1)
        {
            print("fire bolt hit");
            //Destroy(transform.gameObject);
            torchLit.SetActive(true);
            torchUnlit.SetActive(false);
        }
    }
}
