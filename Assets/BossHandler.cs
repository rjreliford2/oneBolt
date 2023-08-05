using UnityEngine;
using System.Collections;

public class BossHandler : MonoBehaviour
{
    public GameObject banner;
    public GameObject boss;
    public GameObject[] bosses;
    public GameObject health;
    public GameObject top;
    public GameObject bottom;
    public GameObject bossDead;
    public GameObject gate;
    public GameObject cam;

    private bool dead = false;


    void OnEnable()
    {
        StartCoroutine(BossCoroutine());
    }

    IEnumerator BossCoroutine()
    {
        for (int i = 0; i < 3; i++)
        {
            banner.SetActive(true);
            yield return new WaitForSeconds(1f);
            banner.SetActive(false);
            yield return new WaitForSeconds(1f);
        }

        // Disable banner and enable boss and health
        boss.SetActive(true);
        health.SetActive(true);
    }

    public void SwitchBoss(int bossNum)
    {
        if (!dead)
        {
            bosses[bossNum].SetActive(false);
            int randomNum = Random.Range(0, 3); // Generates a random number between 0-2 inclusive.
            while (randomNum == bossNum)
            {
                randomNum = Random.Range(0, 3); // Generates a different random number if it's the same as bossNum.
            }
            if(bossNum == 2)
            {
                bosses[1].SetActive(true);
            }
            else if(bossNum == 0)
            {
                bosses[2].SetActive(true);
            }else
            {
                bosses[0].SetActive(true);
            }
            
        }
        else
        {
            Die();
            bosses[bossNum].SetActive(false);
        }
    }

    public void Die()
    {
        // Instantiate bossDeath object at the top and move it to the bottom
        GameObject deathObject = Instantiate(bossDead, top.transform.position, Quaternion.identity);
        //deathObject.GetComponent<DialogueTrigger>().newLevel = true;
        StartCoroutine(MoveDeathObject(deathObject));
    }

    IEnumerator MoveDeathObject(GameObject deathObject)
    {
        float timeElapsed = 0f;
        float totalTime = 1.5f; // Total time for the object to move from top to bottom
        Vector3 startPos = deathObject.transform.position;
        Vector3 endPos = bottom.transform.position;

        // Lerp between the start and end position over the specified time
        while (timeElapsed < totalTime)
        {
            float t = timeElapsed / totalTime;
            deathObject.transform.position = Vector3.Lerp(startPos, endPos, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        deathObject.GetComponent<SpriteRenderer>().flipX = false;
        gate.transform.GetChild(1).gameObject.SetActive(false);
        gate.GetComponent<Gate>().Back();
        cam.GetComponent<FollowCam>().enabled= true;

        // Destroy the gameobject that this script is attached to
        Destroy(gameObject);
    }



    public void GoHome()
    {
        bosses[0].GetComponent<BossDP>().goingHome = true;
        dead = true;
        
    }
}
