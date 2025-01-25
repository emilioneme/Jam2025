using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOxygen : MonoBehaviour
{
    [Header("Health Bar")]
    public int maxHealth = 100; // Maximum health
    [SerializeField]
    private int currentHealth; // Current health

    Rigidbody rb;
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
            Die(false);
        }
    }


    void AddOxygen(int amount)
    {
        currentHealth += amount;
    }

    void TakeOxygen(int amount)
    {
        currentHealth -= amount;
    }


    void Die(bool bitenByEnemy = false)
    {
        gameObject.GetComponent<PlayerMovement>().canMove = true;
        StartCoroutine(DeathScreen());

    }


    IEnumerator DeathScreen()
    {
        yield return new WaitForSeconds(3f);
        gameObject.GetComponent<PlayerMovement>().canLook = false;
    }

    void OnCollisionEnter(Collision collider)
    {

        if(collider.transform.CompareTag("Enemy"))
        {
            Die(true);
        }   

        /*
        if(collider.transform.CompareTag("OxygenPack"))
        {
            AddOxygen(10);
        }
        //*/
    }

}
