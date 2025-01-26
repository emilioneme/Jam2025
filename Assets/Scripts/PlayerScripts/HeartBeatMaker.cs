using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatMaker : MonoBehaviour
{
    [SerializeField]
    public AudioClip heartbeatSound;
    public float bpm;
    private IEnumerator HeartbeatCoroutine()
    {
        // Create a temporary audio source to control the volume
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = heartbeatSound;

        while (true)
        {
             // First strong thump (systole)
            audioSource.volume = 0.8f;  // Strong sound
            audioSource.PlayOneShot(heartbeatSound);
            yield return new WaitForSeconds(1.0f * 1f / bpm); // Full beat time

            // Short pause after first thump (diastole)
            yield return new WaitForSeconds(0.2f * 1f / bpm); // Adjust as necessary

            // Second weaker thump (still part of the heartbeat cycle)
            audioSource.volume = 0.7f; // Slightly softer sound
            audioSource.PlayOneShot(heartbeatSound);
            yield return new WaitForSeconds(0.5f * 1f / bpm); // Half a beat for the resting phase

            // Longer pause before repeating the pattern
            yield return new WaitForSeconds(0.3f * 1f / bpm); // Additional pause to simulate relaxed state
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HeartbeatCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void increaseBPM(){
        bpm = bpm + 0.33f;
    }
}
