using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;

public class LightOrbs : MonoBehaviour
{
    public int lOrb = 20;
    public float amplitude = 0.5f; // Set the amplitude of the oscillation
    public float frequency = 2f; // Set the frequency of the oscillation

    private float startY;

    private void Start()
    {
        startY = transform.position.y; // Record the starting Y position of the object
    }

    private void Update()
    {
        // Calculate the new Y position based on a sine wave
        float newY = startY + amplitude * Mathf.Sin(Time.time * frequency);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Stats>().lightOrbs += lOrb;
            Destroy(this.gameObject);
        }
    }
}