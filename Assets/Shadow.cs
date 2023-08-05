using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    private Animator animator;
    private bool isMoving = false;
    public float moveSpeed;

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (isMoving)
        {
            Vector3 moveDirection = new Vector3(moveSpeed * Time.deltaTime, 0f, 0f); // calculate the move direction
            transform.Translate(moveDirection); // move the character to the right
        }
    }

    void OnEnable()
    {

        StartCoroutine(MoveRight());
    }

    IEnumerator MoveRight()
    {
        yield return new WaitForSeconds(3.5f);
        GetComponent<SpriteRenderer>().flipX = false;
        animator.SetBool("Running", true);
        isMoving = true;

    }
}
