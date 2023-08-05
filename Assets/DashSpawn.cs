using System.Collections;
using UnityEngine;

public class DashSpawn : MonoBehaviour
{
    public GameObject bDash;
    public int direction;
    public float speed;
    public GameObject arrow;
    private float damage;
    public int point;

    void Start()
    {
        damage = GetComponentInParent<BossStats>().damage;
    }

    private void OnEnable()
    {
        StartCoroutine(ActivateArrow());
    }

    private IEnumerator ActivateArrow()
    {

        arrow.SetActive(true);
        yield return new WaitForSeconds(1f);
        arrow.SetActive(false);
        
        

        Vector2 spawnDirection = Vector2.zero;
        switch (direction)
        {
            case 1:
                spawnDirection = Vector2.right;
                break;
            case 2:
                spawnDirection = Vector2.up;
                break;
            case 3:
                spawnDirection = Vector2.down;
                break;
            case 4:
                spawnDirection = Vector2.left;
                break;
        }

        GameObject dash = Instantiate(bDash, transform.position, Quaternion.identity);
        dash.GetComponent<BossMelee>().damage = damage;
        Destroy(dash, 4);
        Rigidbody2D rb = dash.GetComponent<Rigidbody2D>();
        rb.velocity = spawnDirection * speed;
        GetComponentInParent<BossDash>().Spawn(point);
        gameObject.SetActive(false);
    }
}