using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOxygen : MonoBehaviour
{
    [Header("Health Bar")]
    public Slider slider; // The health bar slider
    public int maxHealth = 100; // Maximum health
    private int currentHealth; // Current health


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Set the initial health
        slider.maxValue = maxHealth; // Set the slider's max value
        slider.value = currentHealth; // Initialize the slider's value
        StartCoroutine(DecreaseHealthOverTime()); // Start the coroutine
    }
    
    void Update()
    {

    }

    // Coroutine to decrease health by 1 every second
    IEnumerator DecreaseHealthOverTime()
    {
        while (currentHealth > 0)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            currentHealth--; // Decrease health by 1
            slider.value = currentHealth; // Update the slider
        }

        // Optional: Handle what happens when health reaches 0
        Debug.Log("Health depleted!");
    }


    void AddOxygen(int amount)
    {
        currentHealth += amount;
    }

    void OnCollisionEnter(Collision collider)
    {
        if(collider.transform.CompareTag("OxygenPack"))
        {
            AddOxygen(10);
        }
    }

}
