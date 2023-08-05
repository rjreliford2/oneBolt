using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetractableSpikes : MonoBehaviour
{
    public int raiseSpeed;
    public int dropSpeed;
    public int distance;
    private Vector2 origTarget;
    private Vector2 target;
    private bool drop;
    // Start is called before the first frame update
    void Start()
    {
        origTarget = new Vector2(transform.localPosition.x, transform.localPosition.y);
        target = new Vector2(transform.localPosition.x, transform.localPosition.y + distance);
        drop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector2)transform.localPosition == origTarget)
        {
            drop = false;
        } else if ((Vector2)transform.localPosition == target)
        {
            drop = true;
        }
        if (!drop)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, target, raiseSpeed*Time.deltaTime);
            //drop = true;
        } else if (drop)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, origTarget, dropSpeed*Time.deltaTime);
        }
        /*
        else if (drop && (Vector2)transform.position == target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed*Time.deltaTime);
            drop = false;
        }
        */
    }
}
