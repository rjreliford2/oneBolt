using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    private bool paused = false;

    public GameObject player;
    private Stats playerStats;
    public GameObject elementWheel;
    public GameObject HUD;
    public GameObject pauseMenu;
    public GameObject deathScreen;
    public GameObject shopMenu;
    public GameObject completeMenu;
    private int currentMenu = 0;
    public AudioClip lvlMusic;
    public AudioClip deathMusic;
    public AudioSource bgMusic;


    private void Start()
    {
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<Stats>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Handler(2);
        }
    }

    public void Handler(int menu)
    {
        if (currentMenu == menu || currentMenu == 0)
        {
            if (paused) // Resume if the same menu button is hit
            {
                Time.timeScale = 1f;

                if (menu == 3)
                {
                    playerStats.currentLives--;
                    playerStats.currHealth = playerStats.maxHealth;
                    bgMusic.clip = lvlMusic;
                    bgMusic.Play();
                }

                elementWheel.GetComponent<Animator>().SetBool("Open", false);
                elementWheel.SetActive(false);
                pauseMenu.SetActive(false);
                deathScreen.SetActive(false);
                shopMenu.SetActive(false);
                HUD.SetActive(true);
                currentMenu = 0;
                paused = false;
                player.transform.GetChild(0).gameObject.SetActive(true);
            }
            else // Pause
            {
                currentMenu = menu;
                paused = true;
                player.transform.GetChild(0).gameObject.SetActive(false);

                if (currentMenu == 1) // Element Wheel
                {
                    elementWheel.SetActive(true);
                    elementWheel.GetComponent<Animator>().SetBool("Open", true);
                }
                else if (currentMenu == 2) // Pause Menu
                {
                    pauseMenu.SetActive(true);
                }
                else if (currentMenu == 3) // Death Screen
                {
                    deathScreen.SetActive(true);
                    bgMusic.clip = deathMusic;
                    bgMusic.Play();
                }
                else if (currentMenu == 4) // Shop
                {
                    shopMenu.SetActive(true);
                }
                else // Complete Menu
                {
                    if (PlayerPrefs.GetInt("oneRun", 0) == 1 ? true : false)
                    {
                        float elapsedTime = Time.time - PlayerPrefs.GetFloat("startTime");
                        PlayerPrefs.SetFloat("totalTime", elapsedTime + PlayerPrefs.GetFloat("totalTime"));
                        TimeSpan timeSpan = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("totalTime"));
                        string temp = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
                        completeMenu.transform.Find("Timer").GetComponent<TextMeshProUGUI>().text = temp;
                    }
                    else
                    {
                        completeMenu.transform.Find("Timer").GetComponent<TextMeshProUGUI>().text = "Start From beginning to get total Time!";
                    }
                    completeMenu.SetActive(true);
                }
                HUD.SetActive(false);
                Time.timeScale = 0f;
            }
        }
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;

        PlayerPrefs.SetFloat("currHealth", playerStats.currHealth);
        PlayerPrefs.SetFloat("maxHealth", playerStats.maxHealth);
        PlayerPrefs.SetFloat("lightOrbs", playerStats.lightOrbs);
        PlayerPrefs.SetInt("currLives", playerStats.currentLives);
        PlayerPrefs.SetFloat("damage", playerStats.damage);
        PlayerPrefs.SetFloat("kDamage", playerStats.knifeDamage);
        PlayerPrefs.SetFloat("speed", playerStats.speed);
        PlayerPrefs.SetFloat("jumpH", playerStats.jumpHeight);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
