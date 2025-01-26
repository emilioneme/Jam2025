using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerOxygen : MonoBehaviour
{
    [Header("DeathCanvas")]
    [SerializeField]
    Image BloodImage;

    [SerializeField]
    Image DeathImage;
    [SerializeField]
    float bloodDuration = 3;
    [SerializeField]
    float deathScreenDuration = 4;

    [SerializeField]
    float paralysisDuration = 2;

    [Header("Health Bar")]
    public int maxHealth = 100; // Maximum health
    [SerializeField]
    public int currentHealth; // Current health

    bool isDead;
    Rigidbody rb;

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth; // Set the initial health
        StartCoroutine(DecreaseHealthOverTime()); // Start the coroutine
    }
    
    void Update()
    {

    }

    // Coroutine to decrease health by 1 every second
    IEnumerator DecreaseHealthOverTime()
    {
        while (currentHealth >= 0)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            currentHealth--; // Decrease health by 1
        }

        // Optional: Handle what happens when health reaches 0
        if(currentHealth <= 0)
        {
            if(!isDead)
            {
                Die();
            }
        }
    }


    IEnumerator Die()
    {
        isDead = true;
        // Disable player movement and enable gravity
        gameObject.GetComponent<PlayerMovement>().canMove = false;
        rb.useGravity = true;

        // Wait for the paralysis duration
        yield return new WaitForSeconds(paralysisDuration);

        // Disable player looking
        gameObject.GetComponent<PlayerMovement>().canLook = false;

        // Set BloodImage transparency
        Color bloodColor = BloodImage.color;
        bloodColor.a = .33f; // Fully opaque
        BloodImage.color = bloodColor;

        // Wait for blood duration
        yield return new WaitForSeconds(bloodDuration);

        // Set DeathImage transparency
        Color deathColor = DeathImage.color;
        deathColor.a = 1f; // Fully opaque
        DeathImage.color = deathColor;

        // Set BloodImage transparency
        bloodColor = BloodImage.color;
        bloodColor.a = 0f; // Fully opaque
        BloodImage.color = bloodColor;

        // Wait for death screen duration
        yield return new WaitForSeconds(deathScreenDuration);

        // Load the menu scene
        SceneManager.LoadScene("THEMENU");
    }

    void OnCollisionEnter(Collision collider)
    {

        if(collider.transform.CompareTag("Enemy"))
        {
            StartCoroutine(Die());
        }   

    }

}
