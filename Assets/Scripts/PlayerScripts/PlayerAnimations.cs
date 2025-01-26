using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField]
    Animator Animator;

    [SerializeField]
    float idleVelocityMax;
    [SerializeField]
    float speedingVelocityMin;

    Rigidbody rb;
    public enum PlayerAnimationStates
    {
        Forward,
        OxygenCheck,
        Backward, 
        None,
        Dead
        
    }

    public PlayerAnimationStates currentAnimationState = PlayerAnimationStates.None;


    // Update is called once per frame
    void Update()
    {   
        //CHangeStates
        if(!gameObject.GetComponent<PlayerMovement>().canMove)
        {
              currentAnimationState = PlayerAnimationStates.Dead;
        }
        else if(Input.GetKey(KeyCode.Tab))
        {
            currentAnimationState = PlayerAnimationStates.OxygenCheck;
        }
        else if(Input.GetAxis("Vertical") > 0)
        {
            currentAnimationState = PlayerAnimationStates.Forward;
        }
        else if(Input.GetAxis("Vertical") < 0 || Input.GetAxis("Horizontal") != 0)
        {
            currentAnimationState = PlayerAnimationStates.Backward;
        }
        else
        {
            currentAnimationState = PlayerAnimationStates.None;
        }

        ///////////////////////////////////////////////////////////////////////////////////
        //Set Animator bools
        if(currentAnimationState == PlayerAnimationStates.Forward)
        {
            Animator.SetBool("isSwimming", true);
        }else
        {
            Animator.SetBool("isSwimming", false);
        }

        if(currentAnimationState == PlayerAnimationStates.Backward)
        {
            Animator.SetBool("isIdle", true); //IS USING IDLE ANIMATION!!!
        }else
        {
            Animator.SetBool("isIdle", false);
        }

        if(currentAnimationState == PlayerAnimationStates.OxygenCheck)
        {
            Animator.SetBool("isOxygenCheck", true);
        }else
        {
            Animator.SetBool("isOxygenCheck", false);
        }

        if(currentAnimationState == PlayerAnimationStates.None)
        {
            Animator.SetBool("isNone", true);
        }else
        {
            Animator.SetBool("isNone", false);
        }

        if(currentAnimationState == PlayerAnimationStates.Dead)
        {
            Animator.SetBool("isDead", true);
        }else
        {
            Animator.SetBool("isDead", false);
        }

        
    }
}
