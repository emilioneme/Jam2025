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
    Canvas DeathCanvas;
    [SerializeField]
    float DeathScreenDuration = 6;

    [Header("Health Bar")]
    public int maxHealth = 100; // Maximum health
    [SerializeField]
    private int currentHealth; // Current health

    Rigidbody rb;

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        DeathCanvas.enabled = false;
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
            Die();
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


    void Die()
    {
        gameObject.GetComponent<PlayerMovement>().canMove = false;
        StartCoroutine(DeathScreen());
        rb.useGravity = true;
    }


    IEnumerator DeathScreen()
    {
        yield return new WaitForSeconds(3f);
        gameObject.GetComponent<PlayerMovement>().canLook = false;
        DeathCanvas.enabled = true;
        StartCoroutine(LoadSceneAsync("THEMENU"));
    }

    // Coroutine to handle asynchronous scene loadingSSS
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(DeathScreenDuration);

        // Optional: Hide the loading screen once done
        SceneManager.LoadScene(sceneName);
    }

    void OnCollisionEnter(Collision collider)
    {

        if(collider.transform.CompareTag("Enemy"))
        {
            Die();
        }   

    }

}
