using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class BossRain : MonoBehaviour
{
    public GameObject bossSprite;
    public GameObject homeObject;
    public GameObject rainObject;
    public GameObject dispenserObject;
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireballSpeed = 10f;
    public float journeyTime;
    public float dispenseTime;
    public BossHandler bossHandler;


    public void OnEnable()
    {
        bossSprite.transform.position = homeObject.transform.position;
        StartCoroutine(MoveToRain());
    }

    IEnumerator MoveToRain()
    {

        float startTime = Time.time;
        Vector3 startPosition = bossSprite.transform.position;
        Vector3 endPosition = rainObject.transform.position;

        while (bossSprite.transform.position != endPosition)
        {
            float distCovered = (Time.time - startTime) * journeyTime;
            float fractionOfJourney = distCovered / Vector3.Distance(startPosition, endPosition);
            bossSprite.transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
            yield return null;
        }

        // Call the StartRaining function once the boss sprite reaches the rain object
        StartCoroutine(StartRaining());
    }

    IEnumerator StartRaining()
    {
        // Instantiate the fireball prefab at the fire point and set its direction
        for (int i = 0; i < 20; i++)
        {
            GameObject fireball = Instantiate(fireballPrefab, firePoint.transform.position, Quaternion.identity);
            fireball.transform.localScale = fireball.transform.localScale * .5f;
            Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f)).normalized;
            fireball.GetComponent<Rigidbody2D>().velocity = direction * fireballSpeed;

            yield return new WaitForSeconds(0.2f);
        }

        // Enable the dispenser object and start dispensing for the dispense time
        dispenserObject.SetActive(true);
        StartCoroutine(MoveToHome());
        yield return new WaitForSeconds(dispenseTime);

        dispenserObject.SetActive(false);
        bossHandler.SwitchBoss(1);

    }

    IEnumerator MoveToHome()
    {

        float startTime = Time.time;
        Vector3 startPosition = bossSprite.transform.position;
        Vector3 endPosition = homeObject.transform.position;

        while (bossSprite.transform.position != endPosition)
        {
            float distCovered = (Time.time - startTime) * journeyTime;
            float fractionOfJourney = distCovered / Vector3.Distance(startPosition, endPosition);
            bossSprite.transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
            yield return null;
        }
    }

}
