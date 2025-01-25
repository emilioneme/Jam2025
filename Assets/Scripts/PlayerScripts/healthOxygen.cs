using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthOxygen : MonoBehaviour
{
    [Header("Health Bar")]
    public int maxHealth = 60; // Maximum health
    private int currentOxygen; // Current health
    private float timer = 0f; // Timer to track seconds

    // Start is called before the first frame update
    void Start()
    {
        currentOxygen = maxHealth; // Set the initial health at the beginning of the game

    }

    // call for current oxygen meter of player to display
    public int GetOxygen()
    {
        return currentOxygen;
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
            DecreaseOxygen(); // Call the function to decrease health
        }
    }

    // Function to decrease health ambiently
    void DecreaseOxygen()
    {
        if (currentOxygen > 0)
        {
            currentOxygen--; // Decrease health by 1
            Debug.Log(currentOxygen); // placeholder till we can have the diagetic display of time on player's arm(?)
        }
        else
        {
            // Handle what happens when health reaches 0
            Debug.Log("Ded");
        }
    }

    // Fish fear me, women won't look me in the eyes, no animal on this Earth can stand to be near me. I am alone on this green planet.
    // Public function for calling every time an actor damages the player, since I'm making currentOxygen private.
    public void InjurePlayer(int dmg)
    {
         currentOxygen = Mathf.Max(currentOxygen - dmg, 0); // Prevent going below 0
    }

    // for da bubbles if we ever add any way to increase player oxygen in game
    public void HealPlayer(int amt)
    {
        currentOxygen += amt;
    }
}