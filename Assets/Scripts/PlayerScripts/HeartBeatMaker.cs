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
        while (true)
        {
            Debug.Log("RUNNNING COUROUTINE");
            AudioSource.PlayClipAtPoint(heartbeatSound, new Vector3(0,0,0));
            yield return new WaitForSeconds(1.0f*1f/bpm);
            AudioSource.PlayClipAtPoint(heartbeatSound, new Vector3(0, 0, 0));
            yield return new WaitForSeconds(0.5f * 1f / bpm);
            AudioSource.PlayClipAtPoint(heartbeatSound, new Vector3(0, 0, 0));
            yield return new WaitForSeconds(1.0f * 1f / bpm);
            AudioSource.PlayClipAtPoint(heartbeatSound, new Vector3(0, 0, 0));
            yield return new WaitForSeconds(1.0f * 1f / bpm);
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
}
