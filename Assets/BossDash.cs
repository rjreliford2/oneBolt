using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDash : MonoBehaviour
{
    public GameObject[] points;
    public int spawns;
    public int maxspawns;
    public BossHandler bossHandler;

    private void OnEnable()
    {
        spawns = maxspawns;
        Spawn(0);
    }

    public void Spawn(int point)
    {
        if (spawns >= 1)
        {
            int randomIndex = Random.Range(0, points.Length);
            while(randomIndex != point)
            {
                randomIndex = Random.Range(0, points.Length);
            }
            points[randomIndex].SetActive(true);
            spawns--;
        }
        else
        {
            bossHandler.SwitchBoss(2);
            
        }
    }


}
