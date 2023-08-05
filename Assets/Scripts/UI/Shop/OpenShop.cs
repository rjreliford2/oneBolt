using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    private SpriteRenderer merchSprite;
    private GameManager gameManager;
    private GameObject text;
    private bool inside = false;
    // Start is called before the first frame update
    void Start()
    {
        merchSprite = GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("MainCanvas").GetComponent<GameManager>();
        text = transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inside)
        {
            gameManager.Handler(4);
        }
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inside = true;
            text.SetActive(true);
            if (other.transform.position.x > transform.position.x) merchSprite.flipX = false;
            else merchSprite.flipX = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            text.SetActive(false);
            inside = false;
        }
    }
}
