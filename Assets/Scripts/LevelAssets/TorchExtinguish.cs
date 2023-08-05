using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
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
        if (collision.gameObject.GetComponent<Bolt>().type == 4)
        {
            print("air bolt hit");
            //Destroy(transform.gameObject);
            torchLit.SetActive(false);
            torchUnlit.SetActive(true);
        }
    }
}
