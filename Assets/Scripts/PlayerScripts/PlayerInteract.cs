using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    bool CanWinNow;
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

            switch(eggCounter){

                case 0:
                    pickUp1EggSound = Instantiate(pickUp1EggSoundPrefab);
                    Destroy(pickUp1EggSound, 15f);
                    break;

                case 1:
                    pickUp2EggSound = Instantiate(pickUp2EggSoundPrefab);
                    Destroy(pickUp2EggSound, 15f);
                    break;

                case 2:
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
                Debug.Log("You win!");
                Debug.Log("Eggs collected: " + eggCounter + "/" + maxEggs);
            } else {
                //TODO TELL THEM THEY CANT WIN YET
                Debug.Log("Need to get egg first.");
            }
        }
    }
}
