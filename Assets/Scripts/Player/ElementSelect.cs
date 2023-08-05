using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ElementSelect : MonoBehaviour
{
    public GameObject baseBolt;
    public GameObject iceBolt;
    public GameObject fireBolt;
    public GameObject earthBolt;
    public GameObject airBolt;
    public GameObject elementWheel;
    
    public Animator elementAnim;
    private GameManager gameManager;
    private GameObject crossbow;
    private bool open = false;


    private void Start()
    {
        gameManager = GameObject.Find("MainCanvas").GetComponent<GameManager>();
        crossbow = GameObject.Find("CrossBow");

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && crossbow.GetComponent<CrossBow>().Bolt != null)
        {
            gameManager.Handler(1);
            open = true;
            
        }
        if (Input.GetKeyUp(KeyCode.Q) && open)
        {
            gameManager.Handler(1);
            open = false;
        }
    }

    public void UpdateElement(int elementID)
    {
        if(elementID == 0)
        {
            crossbow.GetComponent<CrossBow>().Bolt = baseBolt;
        }
        if (elementID == 1)
        {
            crossbow.GetComponent<CrossBow>().Bolt = fireBolt;
        }
        if (elementID == 2)
        {
            crossbow.GetComponent<CrossBow>().Bolt = earthBolt;
        }
        if (elementID == 3)
        {
            crossbow.GetComponent<CrossBow>().Bolt = iceBolt;
        }
        if (elementID == 4)
        {
            crossbow.GetComponent<CrossBow>().Bolt = airBolt;
        }

    }
}
