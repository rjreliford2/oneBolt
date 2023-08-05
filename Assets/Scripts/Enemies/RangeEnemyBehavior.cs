using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyBehavior: MonoBehaviour
{

    public GameObject Enemy;
    private float AttackTimer = 0;
    private int Turn = -1;
    private int TurnRotation = -1;
    
    private Rigidbody2D rb;
    private Animator animator;
    public HealthBar healthBar;
    public Transform FirePoint;
    
    public float AttackSpeed;
    public float StopPointDistence = 3;
    public float DetectDistence;
    public float MovementSpeed;
    public float Damage;
    public float FirePointRotationSpeed;
    public GameObject bulletPrefab;
    
    public LayerMask mask;

    public AudioClip deathsound;

    public float FireDegree;


    public bool isRangeAttacking = false;
    public bool isDying = false;



    public bool IsFireDegreeChange;
    public float MaxFireDegree;


    // Update is called once per frame

    void Start(){
        FirePoint.transform.rotation = Quaternion.Euler(0, 180, FireDegree);
        bulletPrefab.GetComponent<BulletMove>().setDamage(Damage);
         rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

    }
    
    void Update()
    {
        if (healthBar.slider.value <= healthBar.slider.minValue){
            isDying = true;
            AudioSource.PlayClipAtPoint(deathsound, transform.position);
        }     
        if (!isDying){
            AttackTimer += Time.deltaTime;               
            if (AttackTimer > 0.0f){
                AttackTimer = 0.0f;
            }
            if(!detectPlayer()){
                Patroling();
                isRangeAttacking = false;                   
            }else if(AttackTimer >= 0){
                AttackTimer -= AttackSpeed;
                Attack();
                isRangeAttacking = true;                   
            }
        }
    }

    bool detectPlayer(){
        RaycastHit2D[] hit_right = Physics2D.RaycastAll(Enemy.transform.position, Vector2.right);
        RaycastHit2D[] hit_left = Physics2D.RaycastAll(Enemy.transform.position, Vector2.left);


        foreach (var hit in hit_right)
        {
            if (hit.distance <= DetectDistence && hit.transform.gameObject.name == "Player"){
                Enemy.transform.rotation = Quaternion.Euler(0, 180f, 0);
                Turn = 0;
                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                audioSource.Play();
                return true;
            }
        }
        foreach (var hit in hit_left)
        {
        if (hit.distance <= DetectDistence && hit.transform.gameObject.name == "Player"){
                Enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
                Turn = 180;
                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                audioSource.Play();
                return true;
            }
        } 
        return false;
    }

    void Patroling(){
        RaycastHit2D[] hit_right = Physics2D.RaycastAll(Enemy.transform.position, Vector2.right);
        RaycastHit2D[] hit_left = Physics2D.RaycastAll(Enemy.transform.position, Vector2.left);
        foreach (var hit in hit_right)
        {
            if (hit.distance <= StopPointDistence && hit.transform.gameObject.tag == "StopPoint"){
                Enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
                FirePoint.transform.rotation = Quaternion.Euler(0,0,0-FireDegree);
                Turn = 0;             
            }
        }

        foreach (var hit in hit_left)
        {
            if (hit.distance <= StopPointDistence && hit.transform.gameObject.tag == "StopPoint"){
                Enemy.transform.rotation = Quaternion.Euler(0, 180f, 0);
                FirePoint.transform.rotation = Quaternion.Euler(0,0,180-FireDegree);
                Turn = 180;               
            }
        }
        if (Turn == 180){
                Enemy.transform.position += Vector3.right * MovementSpeed * Time.deltaTime;
        }else{
                Enemy.transform.position += Vector3.left * MovementSpeed * Time.deltaTime;
        }

    }

    void Attack(){
        Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);    
        if (IsFireDegreeChange){
            if (TurnRotation == -1){
                if(FireDegree <= MaxFireDegree){
                    TurnRotation = 1;
                }else{
                    FireDegree -= Time.deltaTime * FirePointRotationSpeed;
                }
            }else if (TurnRotation == 1){
                if(FireDegree >= 0 ){
                    TurnRotation = -1;
                }else{
                    FireDegree += Time.deltaTime * FirePointRotationSpeed;
                }
            }         
        }
        FirePoint.transform.rotation = Quaternion.Euler(0, 180 * TurnRotation, Turn + FireDegree);
    }   
}
