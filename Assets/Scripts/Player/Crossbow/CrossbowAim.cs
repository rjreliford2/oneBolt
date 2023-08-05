using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrossBow : MonoBehaviour
{
    public GameObject CBSprite;
    public GameObject Target;
    public GameObject knifeRange;
    public GameObject BoltPrefab;
    public GameObject Bolt;
    public GameObject currentBolt;
    public GameObject slash;
    public Transform Tip;
    public Sprite full;
    public Sprite empty;
    public Sprite knifeSprite;

    private float damage;
    private float knifeDamage;
    private Transform CBSpriteTransform;
    private Transform TargetTransform;
    private SpriteRenderer CBSpriteRenderer;
    private GameObject knife;
 

    private void Start()
    {
        Stats playerStats = transform.parent.GetComponent<Stats>();
        damage = playerStats.damage;
        knifeDamage = playerStats.knifeDamage;
        CBSpriteTransform = CBSprite.transform;
        TargetTransform = Target.transform;
        CBSpriteRenderer = CBSprite.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
 
        Rotate();
        Fire();
        Retrieve();
    }

    private void Rotate()
    {
        Vector3 targetPos = TargetTransform.position;
        Vector3 direction = targetPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (transform.position.x > targetPos.x)
        {
            CBSpriteRenderer.flipY = true;
        }
        else
        {
            CBSpriteRenderer.flipY = false;
        }
    }

    private void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Bolt != null)
            {
                currentBolt = Instantiate(Bolt, Tip.position, transform.rotation);
                currentBolt.GetComponent<Bolt>().SetDamage(damage);
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = empty;
                Bolt = null;
                
            }
            else
            {
                Collider2D[] colliders = Physics2D.OverlapBoxAll(knifeRange.transform.position, knifeRange.GetComponent<BoxCollider2D>().size, knifeRange.transform.rotation.z);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject.CompareTag("Enemy"))
                    {
                        GameObject other = collider.gameObject;
                        other.GetComponent<EnemyStats>().TakeDamage(knifeDamage, 0f, 0f, 1f, this.transform.position);
                        knife = Instantiate(slash, knifeRange.transform.position, transform.rotation);
                        Destroy(knife, .1f);
                    }
                }


            }
        }
        if(knife != null) transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null; 
        else
        {
            if(Bolt != null) transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = full;
            else transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = empty;
        }
    }

    private void Retrieve()
    {
        if (Input.GetKeyDown(KeyCode.R) && transform.parent.GetComponent<Stats>().lightOrbs > 1f)
        {
            if (currentBolt != null)
            {
                transform.parent.GetComponent<Stats>().lightOrbs -= transform.parent.GetComponent<Stats>().rcost;
                Destroy(currentBolt);
                Reload();
            }
        }
    }

    public void Reload()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = full;
        Bolt = BoltPrefab;
        //play sound
    }



}