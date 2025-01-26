using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
     [SerializeField]
    Image WinImage;

    [SerializeField]
    TMP_Text EggsCollectedText;

    [SerializeField]
    bool CanWinNow;
    [SerializeField]
    int eggCounter;
    int maxEggs = 3;
    [SerializeField]
    GameObject pickUp1EggSoundPrefab;
    
    GameObject pickUp1EggSound;

    [SerializeField]
    GameObject pickUp2EggSoundPrefab;
    GameObject pickUp2EggSound;

    [SerializeField]
    GameObject pickUp3EggSoundPrefab;
    GameObject pickUp3EggSound;

    [SerializeField]
    GameObject EnterLevelIntroSoundPrefab;
    GameObject EnterLevelIntroSound;

    [SerializeField]
    GameObject DoorOpenSoundPrefab;
    GameObject DoorOpenSound;


    [SerializeField]
    GameManager gameManager;
    // Start is called before the first frame update

    void Awake() {
        CanWinNow = false;
    }
    void Start()
    {
        DoorOpenSound = Instantiate(DoorOpenSoundPrefab);
        Destroy(DoorOpenSound, 24f);

        EnterLevelIntroSound = Instantiate(EnterLevelIntroSoundPrefab);
        Destroy(EnterLevelIntroSound, 24f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Egg"))
        {
            CanWinNow = true;
            Destroy(other.gameObject);
            eggCounter = eggCounter + 1;

            gameManager.playerObject.GetComponent<HeartBeatMaker>().increaseBPM();

            switch(eggCounter){

                case 1:
                    pickUp1EggSound = Instantiate(pickUp1EggSoundPrefab);
                    Destroy(pickUp1EggSound, 15f);
                    break;

                case 2:
                    pickUp2EggSound = Instantiate(pickUp2EggSoundPrefab);
                    Destroy(pickUp2EggSound, 15f);
                    break;

                case 3:
                    pickUp3EggSound = Instantiate(pickUp3EggSoundPrefab);
                    Destroy(pickUp3EggSound, 15f);
                    break;

                default:
                    Debug.Log("I have no idea how this happened. Wrong egg counter.");
                    break;
            }

            gameManager.enemyObject.GetComponent<EnemyMovement>().speedUp();
        }

        if(other.CompareTag("Exit")){
            if(CanWinNow == true){
                //TODO WIN CON LOGIC
                EggsCollectedText.text = "Eggs collected: " + eggCounter + "/" + maxEggs;
                // Set DeathImage transparency
                Color winColor = WinImage.color;
                winColor.a = 1f; // Fully opaque
                WinImage.color = winColor;
                Debug.Log("You win!");
                Debug.Log("Eggs collected: " + eggCounter + "/" + maxEggs);
                StartCoroutine(DeleayedGoToMenu());
            } else {
                //TODO TELL THEM THEY CANT WIN YET
                Debug.Log("Need to get egg first.");
            }
        }
    }

    IEnumerator DeleayedGoToMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("THEMENU");
    }
}
