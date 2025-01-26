using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    int minRandomOffset = -2;
    [SerializeField]
    int maxRandomOffset = 2;

    int randomOffset;

    [SerializeField]
    PlayerOxygen playerOxygen;

    [SerializeField]
    Sprite happySptire;
    [SerializeField]
    Sprite panicSptire;
    [SerializeField]
    Sprite deadSptire;

    [SerializeField]
    Image warningImage;

    [SerializeField]
    TMP_Text timerText;
    public enum WarningState
    {
        Happy,
        Panic,
        Dead,
    }

    public WarningState currentState = WarningState.Happy;
    private float lastTimeOffsetted = 0;
    [SerializeField]
    private float randomOffsetCooldown = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Handle random offset cooldown
        if (Time.time > lastTimeOffsetted + randomOffsetCooldown)
        {
            lastTimeOffsetted = Time.time;
            randomOffset = Random.Range(minRandomOffset, maxRandomOffset);
        }

        /*
        // Calculate minutes and seconds for timer
        int minutes = playerOxygen.currentHealth / 60;
        int seconds = playerOxygen.currentHealth % 60; // Use modulus for cleaner code

        // Format seconds to always display two digits (e.g., 02)
        string minuteTimer = minutes.ToString() + ":" + seconds.ToString("D2");
        */

        // Calculate oxygen level percentage
        float oxygenLevel = ((float)(playerOxygen.currentHealth + randomOffset) / playerOxygen.maxHealth) * 100;

        // Update text with oxygen level and timer
        timerText.text = oxygenLevel.ToString("F1") + "% o2";


        if(playerOxygen.currentHealth > (playerOxygen.maxHealth / 3) * 2)
        {
            currentState = WarningState.Happy;
        }
        else if(playerOxygen.currentHealth > (playerOxygen.maxHealth / 3))
        {
            currentState = WarningState.Panic;
        }else
        {
            currentState = WarningState.Dead;
        }

        switch(currentState)
        {
            case  WarningState.Happy:
                warningImage.sprite = happySptire;
                break;
            case WarningState.Panic:
                warningImage.sprite = panicSptire;
                break;
            case WarningState.Dead:
                warningImage.sprite = deadSptire;
                break;

        }

    }
}
