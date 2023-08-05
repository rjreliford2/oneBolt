using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rouge_Animaltion : MonoBehaviour
{
    public GameObject Self;
     public MeleeEnemyBehavior meleeEnemyBehavior;
     

     private float time;
     public float timeToDie;
     private Animator animator;
     private bool Dying = false;
    // Start is called before the first frame update
    void Start()
    {
         animator = GetComponentInChildren<Animator>();
    }

    void Update(){
        if (!meleeEnemyBehavior.isDying){

            if(meleeEnemyBehavior.isRangeAttacking){
                    animator.Play("Base Layer.Attack");
                
            }
            else if (meleeEnemyBehavior.isSlashing){
                animator.Play("Base Layer.Slash");
            }
            else {
                animator.Play("Base Layer.Walk");
            }
            

        }
        else{
            Die();
            timeToDie -= Time.deltaTime;
            if(timeToDie <= 0){
                Destroy(meleeEnemyBehavior.Enemy);
            }
        }
            
    }

   

    void Die (){
        if (!Dying){
            animator.Play("Base Layer.Dying");
            Dying = true;
            
        }
    }
}
