using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Animator anim;

    private Queue<string> sentences;
    private DialogueTrigger talker;
    
    void Start()
    {
        sentences= new Queue<string>();
    }

    public void StartDialogue(Dialogue dialouge, GameObject person)
    {
        talker = person.GetComponent<DialogueTrigger>();
        anim.SetBool("isOpen", true);
        sentences.Clear();
        nameText.text = dialouge.characterName;
        foreach(string sentence in dialouge.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentences.Dequeue()));

    }

    IEnumerator TypeSentence(string sentence)
    {
        descriptionText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            descriptionText.text += letter;
            yield return new WaitForSeconds(.05f);
        }
    }

    public void EndDialogue()
    {
        anim.SetBool("isOpen", false);
        talker.EndActions();
    }
}
