using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
        public GameObject Merchant;
        public GameObject ShopUI;
        public KeyCode key;
        public AudioSource audioData;

        public void Start(){
            OnDisable();
        }


       

        // public void Update(){
        //     RaycastHit2D hit_right = Physics2D.Raycast(Merchant.transform.position, Vector2.right);
        //     RaycastHit2D hit_left = Physics2D.Raycast(Merchant.transform.position, Vector2.left);
        //     Debug.DrawRay(Merchant.transform.position, Vector2.right * hit_right.distance, Color.yellow);
        //     Debug.DrawRay(Merchant.transform.position, Vector2.left * hit_left.distance, Color.yellow);
        //     if ((Input.GetKey(key) && hit_right.distance <= 4 && hit_right.transform.gameObject.name == "Player") 
        //     || (Input.GetKey(key) && hit_left.distance <= 4 && hit_left.transform.gameObject.name == "Player")){
        //         OnEnable();

        //     }else if((hit_left.distance > 4 && hit_left.transform.gameObject.name == "Player")  || 
        //     (hit_right.distance> 4 && hit_right.transform.gameObject.name == "Player")){
        //         OnDisable();
        //     }
        // }

        public void OnDisable(){
            ShopUI.SetActive(false);
            audioData.Pause();
        }
        public void OnEnable()
        {
            ShopUI.SetActive(true);
            audioData.Play(0);
        }
}
