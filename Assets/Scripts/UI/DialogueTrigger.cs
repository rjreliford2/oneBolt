using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager dManager;
    public GameObject text;
    public bool inside = false;
    public bool done = false;
    public GameObject wall;
    public GameObject portal;
    public GameObject cam;
    public float movement;
    public float increaseCam = 0;
    public GameObject shadowPlayer;

    private void Start()
    {
        dManager = FindObjectOfType<DialogueManager>();
        text = transform.GetChild(0).gameObject;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inside && !done)
        {
            TriggerDialogue();
            text.SetActive(false);
        }
    }

    public void TriggerDialogue()
    {
        dManager.StartDialogue(dialogue, this.gameObject);
    }

    public void EndActions()
    {
        done = true;
        if (wall != null)
        {
            portal.SetActive(true);
            cam.transform.Translate(Vector2.right * movement);
            cam.GetComponent<Camera>().orthographicSize += increaseCam;
            shadowPlayer.SetActive(true);
            Destroy(wall);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!done)
        {
            inside = true;
            text.SetActive(true);
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !done)
        {
            text.SetActive(false);
            inside = false;
        }
    }
}
