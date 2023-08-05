using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using Unity.Mathematics;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private float jumpHeight;
    private float teleportDistance;
    private float teleportDelay;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool grounded = false;
    public int jumpsLeft = 2;
    private float timeSinceTeleport = 0f;
    public float KnockBackDistance;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        speed = GetComponent<Stats>().speed;
        jumpHeight = GetComponent<Stats>().jumpHeight;
        teleportDistance = GetComponent<Stats>().teleportDistance;
        teleportDelay = GetComponent<Stats>().teleportDelay;
    }

    void Update()
    {

        HandleMovementInput();
        HandleJumpInput();
        HandleTeleportInput();
        animator.SetBool("Grounded", grounded);


    }


    void HandleMovementInput()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        //animator.SetBool("Grounded", grounded);

        if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("Running", true);
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpsLeft--;
            if (grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                grounded = false;
                animator.SetTrigger("Jump");
            }
            else if (jumpsLeft == 1)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                animator.SetTrigger("Jump");
                grounded = false;

            }
        }
    }

    void HandleTeleportInput()
    {
        // Update timer since last teleport
        timeSinceTeleport += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && timeSinceTeleport >= teleportDelay)
        {
            //Ignore Enemy Layer
            Physics2D.IgnoreLayerCollision(0, 7, true);
            Physics2D.IgnoreLayerCollision(0, 8, true);
            Physics2D.IgnoreLayerCollision(0, 9, true);

            // Calculate teleport position
            Vector3 teleportPos = transform.position + (spriteRenderer.flipX ? Vector3.left : Vector3.right) * teleportDistance;

            // Calculate direction and distance to teleport position
            Vector3 teleportDirection = teleportPos - transform.position;
            float teleportDistanceRemaining = teleportDirection.magnitude;

            // Check if player is already close to the teleport position
            if (teleportDistanceRemaining > 0.1f)
            {
                // Cast multiple rays to find the maximum distance that the player can teleport
                int numRays = 10;
                float raySpacing = teleportDistanceRemaining / numRays;
                Vector3 rayOrigin = transform.position;
                Vector3 rayDirection = teleportDirection.normalized;
                bool hitWall = false;
                float maxTeleportDistance = teleportDistanceRemaining;

                for (int i = 0; i < numRays; i++)
                {
                    RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, maxTeleportDistance, LayerMask.GetMask("Ground"));

                    if (hit.collider != null)
                    {
                        // Hit a wall, limit the maximum teleport distance
                        hitWall = true;
                        maxTeleportDistance = Mathf.Min(maxTeleportDistance, hit.distance - 0.1f);
                    }

                    rayOrigin += rayDirection * raySpacing;
                    teleportDistanceRemaining -= raySpacing;

                    if (teleportDistanceRemaining <= 0f)
                    {
                        // No distance remaining to teleport
                        break;
                    }
                }

                // Teleport the player to the maximum distance allowed
                Vector3 finalTeleportPos = transform.position + teleportDirection.normalized * maxTeleportDistance;
                transform.position = finalTeleportPos;

                // Reset timer and trigger teleport animation
                timeSinceTeleport = 0f;
                animator.SetTrigger("Dash");

                // Make player invulnerable for a short time
                foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
                {
                    Material material = renderer.material;
                    Color color = material.color;
                    color.a = 0.5f;
                    material.color = color;
                }
                StartCoroutine(DisableInvulnerable(GetComponent<Stats>().iFrames));
            }
        }
    }

    IEnumerator DisableInvulnerable(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Physics2D.IgnoreLayerCollision(0, 7, false);
        Physics2D.IgnoreLayerCollision(0, 8, false);
        Physics2D.IgnoreLayerCollision(0, 9, false);
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            Material material = renderer.material;
            Color color = material.color;
            color.a = 1f;
            material.color = color;
        }

    }


    public void UpdateVals()
    {
        speed = GetComponent<Stats>().speed;
        jumpHeight = GetComponent<Stats>().jumpHeight;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")){
            if (collision.gameObject.transform.position.x >= transform.position.x){
                transform.position = new Vector2(transform.position.x-KnockBackDistance, transform.position.y);
            }else {
                transform.position = new Vector2(transform.position.x + KnockBackDistance, transform.position.y);
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > .7f)
                {
                    grounded = true;
                    jumpsLeft = 2;
                }
            }
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            
            grounded = false;
            animator.SetTrigger("Jump");

        }
    }
    
}
