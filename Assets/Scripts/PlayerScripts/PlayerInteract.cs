using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    bool CanWinNow;
    int eggCounter;
    // Start is called before the first frame update

    void Awake() {
        CanWinNow = false;
    }
    void Start()
    {
        
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
        }

        if(other.CompareTag("Exit")){
            if(CanWinNow == true){
                //TODO WIN CON LOGIC
                Debug.Log("You win!");
                Debug.Log("Eggs collected: " + eggCounter);
            } else {
                //TODO TELL THEM THEY CANT WIN YET
                Debug.Log("Need to get egg first.");
            }
        }
    }
}
