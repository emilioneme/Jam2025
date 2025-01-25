using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Mouse Settings")]
    [SerializeField]
    float lookXSensitivity = 2f;
    [SerializeField]
    float lookYSensitivity = 2f;

    [Header("Movement Settings")]
    [SerializeField]
    float forwardSrintingSpeed = 3f;
    [SerializeField]
    float forwardSpeed = 5f;
    [SerializeField]
    float backSpeed = 1f;
    [SerializeField]
    float strafeSpeed = 5f;
    [SerializeField]
    float verticalSpeed = 5f;

    float forwardSpeedCurrent = 5f;

    private float movementHorizontal = 0f;
    private float movementVertical = 0f;
    private float movementUpward = 0f;

    private float lookX = 0f;
    private float lookY = 0f;

    private float pitch = 0f; // Vertical rotation (pitch)

    public bool canMove = true;

    public bool canLook = true;

    [SerializeField]
    Rigidbody rb;

    void Start()
    {
        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if(canMove)
        {
            Movement();
        }

        if(canLook)
        {
           Look(); 
        }
    }

    void Movement()
    {
        forwardSpeedCurrent = Input.GetKey(KeyCode.LeftControl) ? forwardSrintingSpeed : forwardSpeed;
        // Get keyboard inputs for movement
        if(Input.GetAxis("Vertical") < 0)
        {
            movementVertical = Input.GetAxis("Vertical") * backSpeed;
        }
        else
        {
            movementVertical = Input.GetAxis("Vertical") * forwardSpeedCurrent;
        }

        movementHorizontal = Input.GetAxis("Horizontal") * strafeSpeed;
        movementUpward = Input.GetKey(KeyCode.Space) ? verticalSpeed : (Input.GetKey(KeyCode.LeftShift) ? -verticalSpeed : 0f);

        // Combine movement directions
        Vector3 forwardMovement = transform.forward * movementVertical;
        Vector3 strafeMovement = transform.right * movementHorizontal;
        Vector3 verticalMovement = transform.up * movementUpward;

        Vector3 movementForce = forwardMovement + strafeMovement + verticalMovement;

        // Apply movement to Rigidbody
        rb.velocity = movementForce;
    }

    void Look()
    {
        // Get mouse input
        lookX = Input.GetAxis("Mouse X") * lookXSensitivity;
        lookY = Input.GetAxis("Mouse Y") * lookYSensitivity;

        // Rotate the player horizontally (yaw)
        transform.Rotate(Vector3.up * lookX);

        // Adjust the player's vertical rotation (pitch) and clamp it
        pitch -= lookY;
        pitch = Mathf.Clamp(pitch, -80f, 80f);

        // Apply pitch rotation to the player's local rotation
        transform.localRotation = Quaternion.Euler(pitch, transform.localRotation.eulerAngles.y, 0f);
    }
}


