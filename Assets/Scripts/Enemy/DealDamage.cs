using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public int damageAmount = 10; // Amount of damage to deal

    // Detect collision with the player
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the healthOxygen script
        healthOxygen playerOxygen = collision.gameObject.GetComponent<healthOxygen>();

        if (playerOxygen != null)
        {
            // Call InjurePlayer to deal damage
            playerOxygen.InjurePlayer(damageAmount);
            Debug.Log("Dealt " + damageAmount + " damage to the player. Remaining: " + playerOxygen.GetOxygen());
        }
    }
}
