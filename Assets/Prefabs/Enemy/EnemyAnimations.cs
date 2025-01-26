using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField]
    Animator Animator;
    
    Rigidbody rb;

    [SerializeField]
    EnemyMovement enemyMovement;
    public enum EnemyAnimationStates
    {
        Shriek,
        Swim,
        Chase,
        
    }

    public EnemyAnimationStates currentAnimationState = EnemyAnimationStates.Swim;

    void Start()
    {
       currentAnimationState = EnemyAnimationStates.Swim;
    }


    // Update is called once per frame
    void Update()
    {   
       
        //CHangeStates
        if(enemyMovement.LOSToPlayer())
        {
            currentAnimationState = EnemyAnimationStates.Chase;
        }
        else if(enemyMovement.LOSToPlayer() && enemyMovement.swimmingSpeed == enemyMovement.maxSpeed)
        {
            currentAnimationState = EnemyAnimationStates.Shriek;
        }
        else
        {
            currentAnimationState = EnemyAnimationStates.Swim;
        }

        ///////////////////////////////////////////////////////////////////////////////////
        //Set Animator bools
        if(currentAnimationState == EnemyAnimationStates.Swim)
        {
            Animator.SetBool("isSwimming", true);
        }else
        {
            Animator.SetBool("isSwimming", false);
        }

        if(currentAnimationState == EnemyAnimationStates.Shriek)
        {
            Animator.SetBool("isShriek", true);
        }else
        {
            Animator.SetBool("isShriek", false);
        }

        if(currentAnimationState == EnemyAnimationStates.Chase)
        {
            Animator.SetBool("isChase", true);
        }else
        {
            Animator.SetBool("isChase", false);
        }

        
    }
}
