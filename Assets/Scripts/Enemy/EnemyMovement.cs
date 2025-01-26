using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [Header("CooldownThingy")]
    [SerializeField]
    float cooldown = 3f;
    [SerializeField]
    float lastTimeUsed = 0f;


    [SerializeField]
    public Transform TargetTransform;

    [SerializeField]
    Transform PlayerTransform;

    [SerializeField]
    EnemyNodeFinder enemyNodeFinder;

    [SerializeField]
    GameManager gameManager;


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
    float minDistanceToBePredictive;

    bool isMoving = true;

    Vector3 direction;

    float distance;

    List<Node> path;

    float maxSpeed;
    float speedIncrement;


    public enum ChasingStates
    {
        Predictive,
        Direct,
    }


    public ChasingStates currentState = ChasingStates.Predictive;

    [SerializeField]
    Rigidbody rb;

    void Start()
    {
        //TargetTransform = enemyNodeFinder.GetTargetNode().transform;
        rb = GetComponent<Rigidbody>(); // Assign the Rigidbody
        swimmingSpeed = defaultSpeed;
        maxSpeed = gameManager.playerObject.GetComponent<PlayerMovement>().forwardSrintingSpeed;
        speedIncrement = (maxSpeed - defaultSpeed) / 3;
    }

    void Update()
    {
        if (Time.time > lastTimeUsed + cooldown)
        {

            lastTimeUsed = Time.time;

            if (LOSToPlayer())
            {
                TargetTransform = PlayerTransform;
            }
            else
            {
                path = enemyNodeFinder.GetTargetPath();
                TargetTransform = path[0].transform;
            }
        }
        distance = Vector3.Distance(TargetTransform.position, this.transform.position);

        if (TargetTransform == TargetTransform.CompareTag("Player")
        && distance > minDistanceToBePredictive)
        {
            currentState = ChasingStates.Predictive;
        }
        else
        {
            currentState = ChasingStates.Direct;
        }


        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, maxTargetDistance))
        {
            if (hitInfo.collider.transform == TargetTransform && TargetTransform == TargetTransform.CompareTag("Player"))
            {
                //swimmingSpeed = defaultSpeed + 50f * 1.0f/(0.1f+Vector3.Distance(this.transform.position, TargetTransform.position));
                swimmingSpeed = speedingSpeed;
            }
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

    private bool LOSToPlayer()
    {
        Vector3 direction = (gameManager.playerObject.transform.position - this.transform.position).normalized;
        //Debug.DrawRay(this.transform.position, (gameManager.playerObject.transform.position - this.transform.position), Color.blue, cooldown);
        int layerToIgnore = LayerMask.NameToLayer("Node");
        int layerToIgnore2 = LayerMask.NameToLayer("Enemy");
        int combinedLayerMask = (1 << layerToIgnore) | (1 << layerToIgnore2); // Combine both layers
        int layerMask = ~combinedLayerMask; // Invert the combined mask to ignore both layers


        if (Physics.Raycast(transform.position, direction, out RaycastHit hitInfo, Mathf.Infinity, layerMask))
        {
            if (hitInfo.transform.CompareTag("Player"))
            {
                return true;
            }
            return false;
        }
        return false;
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

    public void NextNode()
    {
        Debug.Log("Next Node");
        path.RemoveAt(0);
        if(path.Count > 0){
            TargetTransform = path[0].transform;
        } else {
            path = enemyNodeFinder.GetTargetPath();
            TargetTransform = path[0].transform;
        }
        
    }

    public void speedUp()
    {
        defaultSpeed = defaultSpeed + speedIncrement;
    }
}
