using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Animaltion : MonoBehaviour
{
     public GameObject Self;
     public RangeEnemyBehavior rangeEnemyBehavior;
     public HealthBar healthBar;
     public float LandTrigger;
     private float time;
     private bool isAttacking = false;
     private bool isDying = false;
     public float timeToDie;
     private Animator animator;
    // Start is called before the first frame update
    void Start()
    {     time = rangeEnemyBehavior.AttackSpeed;
         animator = GetComponentInChildren<Animator>();
    }

    void Update(){
        Die();
        if (!isDying){
            if(isAttacking){
                time -= Time.deltaTime;
            }
            if (time == rangeEnemyBehavior.AttackSpeed || time <= 0){
                if(!detectPlayer()){
                 detectGround(); 
                }
            }
        }
        else{
            timeToDie -= Time.deltaTime;
            if(timeToDie <= 0){
                Destroy(rangeEnemyBehavior.Enemy);
            }
        }
            
    }
        
    
    // Update is called once per frame
    void detectGround(){
        var direction = Vector3.down;
        RaycastHit2D[] heightRay = Physics2D.RaycastAll(Self.transform.position, Vector2.down, 7);
        Debug.DrawRay(Self.transform.position, direction, Color.green);
        foreach(var hit in heightRay){
            if (hit.distance >= LandTrigger && hit.transform.gameObject.name == "Ground"){   
                animator.Play("Base Layer.Walk");
            }
        }
    }

    bool detectPlayer(){
        RaycastHit2D[] hit_right = Physics2D.RaycastAll(Self.transform.position, Vector2.right);
        RaycastHit2D[] hit_left = Physics2D.RaycastAll(Self.transform.position, Vector2.left);

        foreach (var hit in hit_right)
        {
            if (hit.distance <= rangeEnemyBehavior.DetectDistence+0.2f  && hit.transform.gameObject.tag == "Player"){
                animator.Play("Base Layer.Attack");
                isAttacking =true;
                time = rangeEnemyBehavior.AttackSpeed;
                return true;
                 
            }
        }

         foreach (var hit in hit_left)
        {
            if (hit.distance <= rangeEnemyBehavior.DetectDistence+0.2f && hit.transform.gameObject.tag == "Player"){
                animator.Play("Base Layer.Attack");
                isAttacking =true;
                time = rangeEnemyBehavior.AttackSpeed;
                return true;
            }
        }
        isAttacking =false;
        return false;
    }

    void Die (){
        //print ("Cur :" +healthBar.slider.value );
       // print ("min :" +healthBar.slider.minValue );

        if (healthBar.slider.value <= healthBar.slider.minValue){
            animator.Play("Base Layer.Died");
            isDying = true;
            
        }
    }
}
