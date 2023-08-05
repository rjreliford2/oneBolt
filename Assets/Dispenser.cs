using System.Collections;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    public GameObject fireballPrefab;   // The prefab for the fireball to be instantiated
    public float interval = 0.25f;       // The time between each instantiation
    public float angle = 45f;           // The angle at which the fireballs are launched (in degrees)
    public float fireChance = 0.5f;     // The chance of firing a fireball (between 0 and 1)

    void OnEnable()
    {
        // Start the coroutine that spawns the fireballs
        StartCoroutine(SpawnFireballs());
    }

    IEnumerator SpawnFireballs()
    {
        while (true)
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(interval);

            // Randomly determine if a fireball should be spawned
            if (Random.value <= fireChance)
            {
                // Instantiate the fireball at the position of the dispenser
                GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

                // Set the angle of the fireball's trajectory
                float down = Random.Range(-angle, angle);
                Vector3 direction = Quaternion.Euler(0, 0, down) * Vector3.down;
                fireball.GetComponent<Rigidbody2D>().velocity = direction.normalized * fireball.GetComponent<Fireball>().speed;
            }
        }
    }
}