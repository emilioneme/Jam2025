using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleInFrontOfPlayer : MonoBehaviour
{
    [SerializeField]
    Transform CameraTransform;

    [SerializeField]
    float multiplier;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = CameraTransform.position;
    }
}
