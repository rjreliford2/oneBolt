using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage_Animaltion : MonoBehaviour
{
    public GameObject Self;
     public RangeEnemyBehavior rangeEnemyBehavior;
     

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
        if (!rangeEnemyBehavior.isDying){

            if(rangeEnemyBehavior.isRangeAttacking){
                    animator.Play("Base Layer.Attack");
                
            }
            else {
                animator.Play("Base Layer.Walk");
            }
            

        }
        else{
            Die();
            timeToDie -= Time.deltaTime;
            if(timeToDie <= 0){
                Destroy(rangeEnemyBehavior.Enemy);
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
