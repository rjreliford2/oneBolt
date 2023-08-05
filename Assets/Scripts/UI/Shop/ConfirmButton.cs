using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ConfirButton : MonoBehaviour
{
    public int[,] shopItems = new int[5,5];
    public float coins;
    public float price;
    public TextMeshProUGUI PriceTXT;
    public TextMeshProUGUI CurCoinsTXT;
    public TextMeshProUGUI BowATK_TXT;
    public TextMeshProUGUI SwordATK_TXT;
    public TextMeshProUGUI Health_TXT;
    //public GameObject ConfirmRef;
    private int ItemID = -1 ;

    // Start is called before the first frame update
    void Start()
    {
        CurCoinsTXT.text =  price.ToString() + " $";

        shopItems[1,1] = 1 ;
        shopItems[1,2] = 2 ;
        shopItems[1,3] = 3 ;
        shopItems[1,4] = 4 ;

        //Price
        shopItems[2,1] = 20;
        shopItems[2,2] = 30;
        shopItems[2,3] = 40;
        shopItems[2,4] = 50;

        // Quantity
        shopItems[3,1] = 0;
        shopItems[3,2] = 0;
        shopItems[3,3] = 0;
        shopItems[3,4] = 0;
        
        CurCoinsTXT.text = coins + " $";
        BowATK_TXT.text = "BOWATK: " + shopItems[3,1].ToString();
        SwordATK_TXT.text = "SWORDATK: " + shopItems[3,2].ToString();
        Health_TXT.text = "HEALTH: " + shopItems[3,3].ToString();
    }


    public void Buy(){
        print (ItemID);
        if (ItemID == -1){
            return;
        }
        
        if (coins >= shopItems[2,ItemID]){
            coins -= shopItems[2,ItemID];
            shopItems[3,ItemID]++;
        }
            BowATK_TXT.text = "BOWATK: " + shopItems[3,1].ToString();
            SwordATK_TXT.text = "SWORDATK: " + shopItems[3,2].ToString();
            Health_TXT.text = "HEALTH: " + shopItems[3,3].ToString();
            CurCoinsTXT.text = coins + " $";
           
        
    }


    public void Select (){
        GameObject ButtonRef = EventSystem.current.currentSelectedGameObject; //GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        ItemID = ButtonRef.GetComponent<ButtonInfo>().ItemID;
        PriceTXT.text = shopItems[2, ItemID].ToString() + " $";
        print(shopItems[2, ItemID].ToString() + " $");
    }
}
