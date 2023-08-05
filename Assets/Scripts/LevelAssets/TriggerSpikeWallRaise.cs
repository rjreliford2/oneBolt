using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpikeWallRaise : MonoBehaviour
{
    public SpikeWall spikeWall;
    public GameObject dropTrigger;
    //public GameObject dropTrigger2;
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            dropTrigger.SetActive(true);
            //dropTrigger2.SetActive(false);
            StartCoroutine(spikeWall.Raise());
        }
    }
}
