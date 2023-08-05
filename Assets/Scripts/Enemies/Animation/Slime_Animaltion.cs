using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Animaltion : MonoBehaviour
{
     public GameObject Self;
     public HealthBar healthBar;
     public MeleeEnemyBehavior meleeEnemyBehavior;

     public float heightTrigger;
     public float LandTrigger;

     private Animator animator;
     private bool IsJumping = false;
    // Start is called before the first frame update
    void Start()
    {
         animator = GetComponentInChildren<Animator>();
    }

    void Update(){
        detectJump();
        Die();
        
    }
    // Update is called once per frame
    void detectJump(){
        var direction = Vector3.down;
        RaycastHit2D[] heightRay = Physics2D.RaycastAll(Self.transform.position, Vector2.down, 7);
        Debug.DrawRay(Self.transform.position, direction, Color.green);
        foreach(var hit in heightRay){
            if (!IsJumping && hit.distance >= heightTrigger && hit.transform.gameObject.name == "Ground"){   
                IsJumping = true;
                animator.Play("Base Layer.Jump");
            }
            else if(IsJumping && hit.distance < heightTrigger && hit.transform.gameObject.name == "Ground"){
                animator.Play("Base Layer.Land");
                IsJumping = false;
                print("Landing");
            }print(IsJumping);
        }
         

    }
    void Die (){
        print ("Cur :" +healthBar.slider.value );
        print ("min :" +healthBar.slider.minValue );

        if (healthBar.slider.value <= healthBar.slider.minValue){
           Destroy(meleeEnemyBehavior.Enemy);
            
        }
    }
}
