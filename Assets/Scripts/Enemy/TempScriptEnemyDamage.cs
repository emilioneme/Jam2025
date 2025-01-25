using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScriptEnemyDamage : MonoBehaviour
{
    public GameObject targetPlayer; // The player GameObject with healthOxygen attached
    public int damageAmount = 10; // Amount of damage to deal

    void Update()
    {
        // Check if the W key is pressed
        if (Input.GetKeyDown(KeyCode.W))
        {
            DealDamage(); // Call the function to deal damage
        }
    }

    void DealDamage()
    {
        if (targetPlayer != null)
        {
            // Access the healthOxygen script
            healthOxygen playerHealth = targetPlayer.GetComponent<healthOxygen>();

            if (playerHealth != null)
            {
                // Call InjurePlayer to deal damage
                playerHealth.InjurePlayer(damageAmount);
                Debug.Log("Dealt " + damageAmount + " damage to the player. Remaining: " + playerHealth.GetOxygen());
            }
            else
            {
                Debug.LogError("The targetPlayer does not have a healthOxygen script attached!");
            }
        }
        else
        {
            Debug.LogError("No targetPlayer assigned!");
        }
    }
}
