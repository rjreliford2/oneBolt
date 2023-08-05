using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class BossDP : MonoBehaviour
{
    public GameObject home;
    public List<GameObject> patrolLocations;
    public float minStopDuration = 5.0f;
    public float maxStopDuration = 15.0f;
    public float homeProbability = 0.4f;
    public GameObject fireball;
    public GameObject element;
    public List<Sprite> elements;
    public GameObject firePoint;
    public GameObject sprite;
    public BossStats stats;
    public Sprite open;
    public Sprite closed;
    public BossHandler bossHandler;
    public bool goingHome = false;

    private float speed;
    private int currentLocationIndex = 0;
    private float stopTimeLeft = 0.0f;
    
    private GameObject player;
    private bool startedSpawning = false;

    private void Start()
    {
        player = GameObject.Find("Player");
        speed = stats.speed;
        currentLocationIndex = 0;

    }

    void OnEnable()
    {
        startedSpawning = false;
        transform.position = home.transform.position;
        stopTimeLeft = Random.Range(minStopDuration, maxStopDuration);
        goingHome = false;
        UpdateElement(Random.Range(0, 4));

        StartCoroutine(MoveToNextLocation());
    }

    IEnumerator MoveToNextLocation()
    {
        while (true)
        {
            Vector3 targetPosition = goingHome ? home.transform.position : patrolLocations[currentLocationIndex].transform.position;

            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            if (goingHome)
            {
                bossHandler.SwitchBoss(0);
                yield break;
            }
            else
            {
                UpdateElement(Random.Range(0, 4));
                int nextLocationIndex = Random.Range(0, patrolLocations.Count);
                while (nextLocationIndex == currentLocationIndex)
                {
                    nextLocationIndex = Random.Range(0, patrolLocations.Count);
                }
                currentLocationIndex = nextLocationIndex;

                stopTimeLeft = Random.Range(minStopDuration, maxStopDuration);
                goingHome = Random.value < homeProbability;

                if (!startedSpawning && currentLocationIndex != 0)
                {
                    startedSpawning = true;
                    StartCoroutine(SpawnFireball());
                }

                yield return new WaitForSeconds(stopTimeLeft);
            }
        }
    }

    IEnumerator SpawnFireball()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));

            // Change sprite to "open" for 0.5 seconds
            sprite.GetComponent<SpriteRenderer>().sprite = open;
            Fireball currFB = Instantiate(fireball, firePoint.transform.position, Quaternion.identity).GetComponent<Fireball>();;
            currFB.damage = stats.damage;
            Vector3 direction = (player.transform.position - currFB.transform.position).normalized;
            currFB.GetComponent<Rigidbody2D>().velocity = direction;
            yield return new WaitForSeconds(0.5f);
            sprite.GetComponent<SpriteRenderer>().sprite = closed;


        }
    }

    void LateUpdate()
    {
        Vector3 playerDirection = player.transform.position - transform.position;
        float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

        if (playerDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(-180, 0, -angle);
            element.transform.position = new Vector3(element.transform.position.x, element.transform.position.y, -1f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
            element.transform.position = new Vector3(element.transform.position.x, element.transform.position.y, -1f);
        }

    }

    private void UpdateElement(int num)
    {
        element.GetComponent<SpriteRenderer>().sprite = elements[num];
        if(num == 0)
        {
            stats.posType = 1;
            stats.negType = 3;
        }else if(num == 1)
        {
            stats.posType = 2;
            stats.negType = 4;
        }
        else if( num == 2)
        {
            stats.posType = 3;
            stats.negType = 1;
        }
        else
        {
            stats.posType = 4;
            stats.negType = 2;
        }
    }

}
