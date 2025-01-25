using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    Transform TargetTransform; 
    
    Vector3 currentTarget;

    Vector3 directTarget;
    Vector3 predictionTarget; 

    [Header("Rotation")]
    [SerializeField]
    float rotationSpeed; 

    [Header("Movement")]
    [SerializeField]
    float swimmingSpeed; 

    [SerializeField]
    float defaultSpeed; 

    [SerializeField]
    float speedingSpeed; 

    [Header("Other")]
    [SerializeField]
    float targetOffset; 

    [SerializeField]
    float maxTargetDistance; 

    [SerializeField]
    float directChaseDistance;

    bool isMoving = true;

    Vector3 direction; 

    float distance;


    public enum ChasingStates
    {
        Predictive,
        Direct
    }

    public ChasingStates currentState = ChasingStates.Predictive;

    [SerializeField]
    Rigidbody rb; 

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Assign the Rigidbody
        swimmingSpeed = defaultSpeed; 
    }

    void Update()
    {
        distance = Vector3.Distance(TargetTransform.position, transform.position);

        if(distance < directChaseDistance)
        {
            currentState = ChasingStates.Direct;
        }else
        {
            currentState = ChasingStates.Predictive;
        }
        

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, maxTargetDistance))
        {
            if (hitInfo.collider.transform == TargetTransform)
                swimmingSpeed = speedingSpeed; // Speed up when the target is directly ahead
            else
                swimmingSpeed = defaultSpeed; // Use default speed otherwise
        }
        

        switch (currentState)
        {
            case ChasingStates.Predictive:
                PredictiveMovement();
                break;
            case ChasingStates.Direct:
                DirectMovement();
                break;
            default:
                DirectMovement();
                break;
        }

    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.velocity = transform.forward * swimmingSpeed;
            RotatePlayer();
        }
    }

    private void DirectMovement()
    {
        directTarget = TargetTransform.position;
        currentTarget = directTarget;

    }

    void PredictiveMovement()
    {
        predictionTarget = TargetTransform.position + TargetTransform.forward * targetOffset;
        currentTarget = predictionTarget;
    }

    void RotatePlayer()
    {
        direction = (currentTarget - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
