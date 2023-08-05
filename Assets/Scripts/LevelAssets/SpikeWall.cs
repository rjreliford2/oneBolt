using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWall : MonoBehaviour
{
    public GameObject player;
    public int speed;
    public GameObject raiseTrigger;
    private Vector2 target;
    private Vector2 origTarget;
    private bool drop;
    // Start is called before the first frame update
    void Start()
    {
        //target = new Vector2(player.transform.position.x, -2.75f);
        origTarget = transform.position;
        target = new Vector2(transform.position.x, raiseTrigger.transform.position.y);
        drop = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (drop)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed*Time.deltaTime);
        }
        
        if ((Vector2)transform.position == target)
        {
            transform.position = Vector2.MoveTowards(transform.position, origTarget, speed*Time.deltaTime);
            drop = false;
        } 
        */
        
        /*else if ((Vector2)transform.position == origTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, origTarget, speed * Time.deltaTime);
            drop = true;
        }
        */
    }
    
    public IEnumerator Drop()
    {
        while ((Vector2)transform.position != target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed*Time.deltaTime);
            yield return null;
        }
        yield return null;
    }

    public IEnumerator Drop(int newDropSpeed)
    {
        while ((Vector2)transform.position != target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, newDropSpeed*Time.deltaTime);
            yield return null;
        }
        yield return null;
    }

    public IEnumerator Raise()
    {
        while ((Vector2)transform.position != origTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, origTarget, speed*Time.deltaTime);
            yield return null;
        }
        yield return null;
    }
}
