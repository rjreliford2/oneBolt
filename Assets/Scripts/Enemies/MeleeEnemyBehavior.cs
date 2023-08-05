using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyBehavior: MonoBehaviour
{

    public GameObject Enemy;
    private Animator animator;
    private int Turn = -1;
    private Rigidbody2D rb;
    public Transform FirePoint;
    public HealthBar healthBar;
    
    public GameObject bulletPrefab;
    public float StopPointDistence = 3;
    public float DetectDistence;
    public float AttackMovementSpeed;

    public float MeleeAttackRange;
    public float Damage;
    public float AttackSpeed;
    public float JumpHeight;
    public float JumpFrequency;
    public float PatrolSpeed;

    public bool isBat   = false;
    public bool isRouge = false;
    public bool isSlashing = false;
    public bool isRangeAttacking = false;
    public bool isMeleeAttacking = false;
    public bool isDying = false;

    public float SlashMovementSpeed;
    public float SlashDistance;
    
    public LayerMask mask;

    public AudioClip deathsound;
    public AudioClip attackSound;

    private float AttackTimer = 1;
    private float JumpTimer = 1;


    // Update is called once per frame

    void Start(){
        if ( Enemy.transform.gameObject.name == "Rouge"){
            bulletPrefab.GetComponent<BulletMove>().setDamage(Damage);
            isRouge = true;
        }else if (Enemy.transform.gameObject.name == "Bat"){
            isBat = true;
        }

        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

    }
    
    void Update(){
        
        if (healthBar.slider.value <= healthBar.slider.minValue){
            print(healthBar.slider.value);
            isDying = true;
            AudioSource.PlayClipAtPoint(deathsound, transform.position);
        }
     
        if (!isDying){
            AttackTimer += Time.deltaTime;
            JumpTimer   += Time.deltaTime;
                
            if (AttackTimer > 1.0f){
                AttackTimer = 1.0f;
            }


            if(JumpTimer > 1.0f) {
                JumpTimer = 1.0f;
            }

            if(!detectPlayer()){
                Patroling();
            
                
            }else if(AttackTimer >= 0){
                AttackTimer -= AttackSpeed;
                Attack();
                    
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Player") && isBat){
            Destroy(gameObject);
        }
        
    }


    bool detectPlayer(){
        RaycastHit2D[] hit_right = Physics2D.RaycastAll(Enemy.transform.position, Vector2.right);
        RaycastHit2D[] hit_left = Physics2D.RaycastAll(Enemy.transform.position, Vector2.left);
        foreach (var hit in hit_right)
        {
            // print("Right: " + hit.distance + " + " + hit.transform.gameObject.name);
            if (hit.distance <= DetectDistence && hit.transform.gameObject.name == "Player"){
                Enemy.transform.rotation = Quaternion.Euler(0, 180f, 0);
                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                audioSource.Play();
                return true;
            }
        }

        foreach (var hit in hit_left)
        {
        if (hit.distance <= DetectDistence && hit.transform.gameObject.name == "Player"){
                Enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                audioSource.Play();
                return true;
            }
        } 
        return false;
    }

    void Patroling(){
        isSlashing = false;
        isRangeAttacking = false;
        isMeleeAttacking = false;
        RaycastHit2D[] hit_right = Physics2D.RaycastAll(Enemy.transform.position, Vector2.right);
        RaycastHit2D[] hit_left = Physics2D.RaycastAll(Enemy.transform.position, Vector2.left);
        foreach (var hit in hit_right)
        {
            // print("Right: " + hit.distance + " + " + hit.transform.gameObject.tag);
            if (hit.distance <= StopPointDistence && hit.transform.gameObject.tag == "StopPoint"){
                Enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
                Turn = 1;
                 
            }
        }

        foreach (var hit in hit_left)
        {
            // print("Left: " + hit.distance + " + " + hit.transform.gameObject.tag);
            if (hit.distance <= StopPointDistence && hit.transform.gameObject.tag == "StopPoint"){
                Enemy.transform.rotation = Quaternion.Euler(0, 180f, 0);
                Turn = -1;
                 
            }
        }
        if (Turn == -1){
                Enemy.transform.position += Vector3.right * PatrolSpeed * Time.deltaTime;
        }else{
                Enemy.transform.position += Vector3.left * PatrolSpeed * Time.deltaTime;
        }
        

    }
    void Attack(){
        //print("Attacking");
       
        if (isRouge){
            RougeAttack();
        }

        else{
            RaycastHit2D[] hit_right = Physics2D.RaycastAll(Enemy.transform.position, Vector2.right);
            RaycastHit2D[] hit_left = Physics2D.RaycastAll(Enemy.transform.position, Vector2.left);
            foreach (var hit in hit_right){
                if (hit.distance <= DetectDistence && hit.transform.gameObject.name == "Player"){
                    Enemy.transform.rotation = Quaternion.Euler(0, 180f, 0);
                    Enemy.transform.position += Vector3.right * AttackMovementSpeed * Time.deltaTime;
                    //print (hit.distance);
                    if (hit.distance <= MeleeAttackRange){
                        isMeleeAttacking = true;
                    }else{
                        isMeleeAttacking = false;

                    }
                } 
            }

            foreach (var hit in hit_left){
                if (hit.distance <= DetectDistence && hit.transform.gameObject.name == "Player"){
                    Enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
                    Enemy.transform.position += Vector3.left * AttackMovementSpeed * Time.deltaTime;
                    if (hit.distance <= MeleeAttackRange){
                        isMeleeAttacking = true;
                    }else{
                        isMeleeAttacking = false;

                    }
                }
            }
        }





        
        if (JumpTimer >= 0){
            rb.velocity = new Vector2(rb.velocity.x, JumpHeight);
            JumpTimer-= JumpFrequency;
        }
        
    }

    void RougeAttack(){
        RaycastHit2D[] hit_right = Physics2D.RaycastAll(Enemy.transform.position, Vector2.right);
        RaycastHit2D[] hit_left = Physics2D.RaycastAll(Enemy.transform.position, Vector2.left);
        foreach (var hit in hit_right){
            if ( hit.distance > SlashDistance  && hit.distance <= DetectDistence && hit.transform.gameObject.name == "Player"){    
                Enemy.transform.rotation = Quaternion.Euler(0, 180f, 0);
                FirePoint.transform.rotation = Quaternion.Euler(0,0, 180f);
                Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
                Enemy.transform.position += Vector3.right * AttackMovementSpeed * Time.deltaTime;
                isRangeAttacking = true;
                isSlashing = false;

            }else if (hit.distance < SlashDistance  && hit.distance <= DetectDistence && hit.transform.gameObject.name == "Player"){
                 Enemy.transform.position += Vector3.right * SlashMovementSpeed * Time.deltaTime;
                 isSlashing = true;
                 isRangeAttacking = false;
            }
        }
        foreach (var hit in hit_left){
            if (hit.distance > SlashDistance && hit.distance <= DetectDistence && hit.transform.gameObject.name == "Player"){
                Enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
                FirePoint.transform.rotation = Quaternion.Euler(0,0, 0);
                Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
                Enemy.transform.position += Vector3.left * AttackMovementSpeed * Time.deltaTime;
                isRangeAttacking = true;
                isSlashing = false;
                
            }else if (hit.distance < SlashDistance  && hit.distance <= DetectDistence && hit.transform.gameObject.name == "Player"){
                Enemy.transform.position += Vector3.left * SlashMovementSpeed * Time.deltaTime;
                isRangeAttacking = false;
                isSlashing = true;
            }
        }
    }
}
