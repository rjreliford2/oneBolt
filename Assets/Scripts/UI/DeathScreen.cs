using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public Button continueButton;
    public Stats player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Stats>();
    }

    private void OnEnable()
    {
        if(player.currentLives <= 0)
        {
            continueButton.interactable = false;
        }

    }
}
