using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [SerializeField]
    GameObject shriekSoundPrefab;
    GameObject shriekSound;

    [SerializeField]
    GameObject shriekSound2Prefab;
    GameObject shriekSound2;

    [SerializeField]
    GameObject shriekSound3Prefab;
    GameObject shriekSound3;

    [Header("CooldownThingy")]
    [SerializeField]
    float cooldown = 5f;
    [SerializeField]
    float lastTimeUsed = 0f;

    int counter = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayShriek(){
        

        if (Time.time > lastTimeUsed + cooldown)
        {
            Debug.Log("mohster sound");

            lastTimeUsed = Time.time;

            switch(counter % 3){
                case 0:
                shriekSound = Instantiate(shriekSoundPrefab);
                Destroy(shriekSound, 7f);
                counter++;
                break;

                case 1:
                shriekSound2 = Instantiate(shriekSound2Prefab);
                Destroy(shriekSound2, 7f);
                counter++;
                break;

                case 2:
                shriekSound3 = Instantiate(shriekSound3Prefab);
                Destroy(shriekSound3, 7f);
                counter++;
                break;

                default:
                shriekSound = Instantiate(shriekSoundPrefab);
                Destroy(shriekSound, 7f);
                counter++;
                break;

            }
        }

    }
}
