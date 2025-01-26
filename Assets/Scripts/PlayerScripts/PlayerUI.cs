using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    float minRandomOffset = -2;
    [SerializeField]
    float maxRandomOffset = 2;

    float randomOffset = Random.Range(-1, 1);

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
        if(Time.time > lastTimeOffsetted + randomOffsetCooldown)
        {
            lastTimeOffsetted = Time.time;
        }

        int minutes = playerOxygen.currentHealth / 60;
        int seconds = playerOxygen.currentHealth - (minutes * 60);
        string minuteTimer = minutes.ToString() + ":" + seconds.ToString();

        float oxygenLevel = playerOxygen.currentHealth + randomOffset;

        timerText.text = oxygenLevel.ToString() + " o2" + "\n" + minuteTimer;


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
