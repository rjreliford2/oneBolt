using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private GameObject player;
    private Stats playerStats;
    private int active = 0;


    public float boltAmount;
    public float knifeAmount;
    public float healthAmount;
    public float speedAmount;
    public float jumpAmount;

    public TextMeshProUGUI currencyText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI resultsText;
    public Button purchase;
    
    void Start()
    {
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<Stats>();
        currencyText.text = "Light Orbs: " + playerStats.lightOrbs.ToString();
        purchase.interactable = false;

    }

    public void Upgrade(int num)
    {
        active = num;
        UpdateText();   
    }

    private void UpdateText()
    {
        float cost = 0;
        currencyText.text = "Light Orbs: " + playerStats.lightOrbs.ToString();
        if (active == 1) //Health
        {
            descriptionText.text = "Description\n This Upgrade will increase the health of the player by 20 units. This does not effect player lives";
            costText.text = "Cost\n" + healthAmount;
            cost = healthAmount;
            resultsText.text = "Results\n" + playerStats.maxHealth + " + 20 =" + (playerStats.maxHealth + 20f);
            

        }
        else if (active == 2) //Bolt
        {
            descriptionText.text = "Description\n This Upgrade will increase the damage of the player's Bolts by 10 units.";
            costText.text = "Cost\n" + boltAmount;
            cost = boltAmount;
            resultsText.text = "Results\n" + playerStats.damage + " + 10 =" + (playerStats.damage + 10f);
        }
        else if (active == 3) //Knife
        {
            descriptionText.text = "Description\n This Upgrade will increase the damage of the player's knife by 5 units.";
            costText.text = "Cost\n" + knifeAmount;
            cost = knifeAmount;
            resultsText.text = "Results\n" + playerStats.knifeDamage + " + 5 =" + (playerStats.knifeDamage + 5f);
        }
        else if (active == 4) //Speed
        {
            descriptionText.text = "Description\n This Upgrade will increase the speed of the player by 10%.";
            costText.text = "Cost\n" + speedAmount;
            cost = speedAmount;
            resultsText.text = "Results\n" + playerStats.speed + " + 5% =" + (Mathf.Round(playerStats.speed * 1.1f));
        }
        else if (active == 5) // Jump
        {
            descriptionText.text = "Description\n This Upgrade will increase the jump height of the player by 10%.";
            costText.text = "Cost\n" + jumpAmount;
            cost = jumpAmount;
            resultsText.text = "Results\n" + playerStats.jumpHeight + " + 5% =" + (Mathf.Round(playerStats.jumpHeight * 1.1f));
        }

        if (cost != 0 && playerStats.lightOrbs >= cost) purchase.interactable = true;
        else purchase.interactable = false;
        
        
    }

    public void Purchase()
    {
        if(active == 1)
        {
            playerStats.maxHealth += 20;
            playerStats.lightOrbs -= healthAmount;
            playerStats.currHealth = playerStats.maxHealth;
            healthAmount *= 2;
            UpdateText();      
            
        }else if(active == 2)
        {
            playerStats.damage += 10;
            playerStats.lightOrbs -= boltAmount;
            boltAmount *= 2;
            UpdateText();        
        }
        else if (active == 3)
        {
            playerStats.knifeDamage += 5;
            playerStats.lightOrbs -= knifeAmount;
            knifeAmount *= 2;
            UpdateText();   
        }
        else if (active == 4)
        {
            playerStats.speed = Mathf.Round(playerStats.speed * 1.1f);
            playerStats.lightOrbs -= speedAmount;
            playerStats.Upgrade(); //work around
            speedAmount *= 2;
            UpdateText();     
        }
        else if (active == 5)
        {
            playerStats.jumpHeight = Mathf.Round(playerStats.jumpHeight * 1.1f);
            playerStats.lightOrbs -= jumpAmount;
            playerStats.Upgrade(); //work around
            jumpAmount *= 2;
            UpdateText();   
        }
    }


}

