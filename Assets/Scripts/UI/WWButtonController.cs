using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WWButtonController : MonoBehaviour
{
    public int Id;
    private Animator anim;
    private GameObject player;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    public void Selected()
    {

        player.GetComponent<ElementSelect>().UpdateElement(Id);

    }

    public void Deselected()
    {
        player.GetComponent<ElementSelect>().UpdateElement(0);

    }

    public void HoverEnter()
    {
        anim.SetBool("Hovered", true);

    }
    public void HoverExit()
    {
        anim.SetBool("Hovered", false);

    }
}
