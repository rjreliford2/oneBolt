using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public Animator anim;
    public GameObject lightOrb;
    public int amount;
    private bool opened = false;
    // Start is called before the first frame update
 

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !opened)
        {
            opened = true;
            anim.SetBool("openChest", true);
            Vector3 currentPosition = transform.position;

            // Set the distance to the left that you want to instantiate the new object
            float distanceToLeft = 2.0f;

            // Calculate the position to the left of the current position
            Vector3 newPosition = currentPosition + Vector3.left * distanceToLeft;

            // Instantiate a new object at the new position
            GameObject lorb = Instantiate(lightOrb, newPosition, Quaternion.identity);
            lorb.GetComponent<LightOrbs>().lOrb = amount;
        }
    }
}
