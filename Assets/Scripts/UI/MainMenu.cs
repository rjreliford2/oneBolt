using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(int scene)
    {
        if(scene > 1) PlayerPrefs.SetInt("oneRun", 0);
        else PlayerPrefs.SetInt("oneRun", 1);

        PlayerPrefs.SetFloat("currHealth", 100);
        PlayerPrefs.SetFloat("maxHealth", 100);
        PlayerPrefs.SetFloat("lightOrbs", 100);
        PlayerPrefs.SetInt("currLives", 3);
        PlayerPrefs.SetFloat("damage", 25);
        PlayerPrefs.SetFloat("kDamage", 10);
        PlayerPrefs.SetFloat("speed", 12);
        PlayerPrefs.SetFloat("jumpH", 10);
        PlayerPrefs.SetFloat("startTime", 0);
        PlayerPrefs.SetFloat("totalTime", 0);

        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
