using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = playerOxygen.currentHealth.ToString() + " O2";


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
