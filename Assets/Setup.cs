using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Stats playerStats = player.GetComponent<Stats>();

        playerStats.currHealth = PlayerPrefs.GetFloat("currHealth", 100);
        playerStats.maxHealth = PlayerPrefs.GetFloat("maxHealth", 100);
        playerStats.lightOrbs = PlayerPrefs.GetFloat("lightOrbs", 100);
        playerStats.currentLives = PlayerPrefs.GetInt("currLives", 3);
        playerStats.damage = PlayerPrefs.GetFloat("damage", 25);
        playerStats.knifeDamage = PlayerPrefs.GetFloat("kDamage", 10);
        playerStats.speed = PlayerPrefs.GetFloat("speed", 12);
        playerStats.jumpHeight = PlayerPrefs.GetFloat("jumpH", 10);


        if (PlayerPrefs.GetInt("oneRun", 0) == 1 ? true : false)
        {
            PlayerPrefs.SetFloat("startTime", Time.time);
        }
        


    }

}
