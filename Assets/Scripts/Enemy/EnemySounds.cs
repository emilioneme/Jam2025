using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [SerializeField]
    GameObject shriekSoundPrefab;
    GameObject shriekSound;

    [Header("CooldownThingy")]
    [SerializeField]
    float cooldown = 5f;
    [SerializeField]
    float lastTimeUsed = 0f;


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

            shriekSound = Instantiate(shriekSoundPrefab);
            Destroy(shriekSound, 5f);
        }

    }
}
