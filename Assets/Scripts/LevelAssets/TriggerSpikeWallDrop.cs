using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpikeWallDrop : MonoBehaviour
{
    public SpikeWall spikeWall;
    public int newDropSpeed;
    public GameObject torchUnlit;
    public GameObject torchLit;
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
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            torchUnlit.SetActive(false);
            torchLit.SetActive(true);
            //spikeWall.Drop();
            StartCoroutine(spikeWall.Drop());
            //yield return new WaitForSeconds(2);
            //Destroy(transform.gameObject);
        }
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //spikeWall.Drop();
            StartCoroutine(spikeWall.Drop(newDropSpeed));
            //yield return new WaitForSeconds(2);
            //Destroy(transform.gameObject);
        }
    }
    /*
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //spikeWall.Drop();
            StartCoroutine(spikeWall.Drop());
            //yield return new WaitForSeconds(2);
            //Destroy(transform.gameObject);
        }
    }
    */
}
