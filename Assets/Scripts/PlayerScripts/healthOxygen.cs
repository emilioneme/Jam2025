using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthOxygen : MonoBehaviour
{
    [Header("Health Bar")]
    public int maxHealth = 100; // Maximum health
    private int currentHealth; // Current health
    private float timer = 0f; // Timer to track seconds

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Set the initial health

    }

    // Update is called once per frame
    void Update()
    {
        // Increment the timer by the time passed since the last frame
        timer += Time.deltaTime;

        // Decrease health if 1 second has passed
        if (timer >= 1f)
        {
            timer = 0f; // Reset the timer
            DecreaseHealth(); // Call the function to decrease health
        }
    }

    // Function to decrease health and update the slider
    void DecreaseHealth()
    {
        if (currentHealth > 0)
        {
            currentHealth--; // Decrease health by 1
            Debug.Log(currentHealth);
        }
        else
        {
            // Optional: Handle what happens when health reaches 0
            Debug.Log("Health depleted!");
        }
    }
}