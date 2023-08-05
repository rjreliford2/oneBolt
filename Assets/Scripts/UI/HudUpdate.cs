using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudUpdate : MonoBehaviour
{
    private Stats player;
    private int numOfLives;
    private Image[] lives;
    private TextMeshProUGUI lightOrbs;
    private TextMeshProUGUI rCost;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private Image[] dLives;
    public GameObject dLife;


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Stats>();
        lives = transform.Find("Lives").GetComponentsInChildren<Image>();
        dLives = dLife.GetComponentsInChildren<Image>();
        lightOrbs = transform.Find("LightOrbs").GetComponentInChildren<TextMeshProUGUI>();
        rCost = transform.Find("RCost").GetComponentInChildren<TextMeshProUGUI>();
        numOfLives = player.maxLives;

       
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateCurrency();
   
    }

    private void UpdateCurrency()
    {
        lightOrbs.text = "Light Orbs\n" + player.lightOrbs.ToString();
        rCost.text = "R-Cost "+player.rcost.ToString();
    }

    private void UpdateHealth()
    {
        for (int i = 0; i < lives.Length; i++)
        {
            if (i < player.currentLives)
            {
                lives[i].sprite = fullHeart;
                dLives[i].sprite = fullHeart;
            }
            else
            {
                lives[i].sprite = emptyHeart;
                dLives[i].sprite = emptyHeart;
            }
            if (i < numOfLives)
            {
                lives[i].enabled = true;
                dLives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
                dLives[i].enabled = false;
            }
        }
    }
}
