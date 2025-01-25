using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField]
    float idleVelocityMax;
    [SerializeField]
    float speedingVelocityMin;

    Rigidbody rb;
    public enum PlayerAnimationStates
    {
        Swimming,
        Speeding,
        Idle,
    }

    public PlayerAnimationStates currentAnimationState = PlayerAnimationStates.Idle;


    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude < idleVelocityMax)
        {
            currentAnimationState = PlayerAnimationStates.Idle;
        }
        else if(rb.velocity.magnitude >= idleVelocityMax && rb.velocity.magnitude <= speedingVelocityMin)
        {
            currentAnimationState = PlayerAnimationStates.Swimming;
        }
        else if(rb.velocity.magnitude > speedingVelocityMin)
        {
            currentAnimationState = PlayerAnimationStates.Speeding;
        }
        
    }
}
