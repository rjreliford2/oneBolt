using UnityEditor;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    private float damage;
    private float amplifier;
    public float speed;
    public float knockBack;
    public int type;
    public int bounce;
    public SpriteRenderer sprite;
    
    private LayerMask groundLayer;
    private LayerMask bounceLayer;
    private LayerMask enemyLayer;
    private LayerMask bossLayer;
    private Vector2 direction;
    private float gravity = 2f;
    private Rigidbody2D boltBody;
    private bool done = false;

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        boltBody = GetComponentInChildren<Rigidbody2D>();
        sprite.sortingOrder = 1;
        direction = transform.right;
        boltBody.AddForce(direction * speed, ForceMode2D.Impulse);
        RotateToDirection(direction);
        groundLayer = LayerMask.GetMask("Ground");
        bounceLayer = LayerMask.GetMask("Bounce");
        enemyLayer = LayerMask.GetMask("Enemy");
        bossLayer = LayerMask.GetMask("Boss");
        Invoke("EnableCollider", .1f);

        
    }
    public void SetDamage(float dam)
    {
        damage = dam;
        amplifier = 1.50f;
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        CheckForCollision();
        RotateToDirection(boltBody.velocity);
    }

    private void ApplyGravity()
    {
        boltBody.AddForce(Vector2.down * gravity);
    }
    private void CheckForCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, boltBody.velocity.normalized, 0.5f, groundLayer | bounceLayer | enemyLayer | bossLayer);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                if (bounce <= 0)
                {
                    boltBody.constraints = RigidbodyConstraints2D.FreezeAll; //out of bounces
                }
                else
                {
                    bounce--;
                    BounceOffSurface(hit.normal);
                }
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Bounce"))
            {
                BounceOffSurface(hit.normal);
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                BounceOffSurface(hit.normal);
                hit.collider.GetComponent<EnemyStats>().TakeDamage(damage, amplifier, type, knockBack, transform.position);
            }else if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Boss") && !done)
            {
                done = true;
                BounceOffSurface(hit.normal);
                Physics2D.IgnoreLayerCollision(gameObject.layer, 7, true);
                hit.collider.GetComponentInParent<BossStats>().TakeDamage(damage, amplifier, type);
            }
        }
    }



    private void BounceOffSurface(Vector2 normal)
    {

        
        Vector2 bounceDirection = Vector2.Reflect(boltBody.velocity.normalized, normal);
        speed *= .75f;
        boltBody.velocity = bounceDirection * speed;
        RotateToDirection(bounceDirection);
        
    }

    private void RotateToDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CrossBow crossbow = other.GetComponentInChildren<CrossBow>();
            if (crossbow != null)
            {
                crossbow.Reload();
                Destroy(gameObject);
            }
        }

    }

    private void EnableCollider()
    {
        BoxCollider2D boltCollider = GetComponent<BoxCollider2D>();
        boltCollider.enabled = true;
        boltCollider.size = new Vector2(5f, 3f); //set pick up distance
        
    }
}
