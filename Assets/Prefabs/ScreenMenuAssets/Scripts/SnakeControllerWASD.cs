using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

// code inspired from https://www.youtube.com/watch?v=iuz7aUHYC_E&ab_channel=OneMinuteGames

public class SnakeControllerWASD : MonoBehaviour
{
    public float MoveSpeed =  5;
    public float SteerSpeed = 100;
    public float BodySpeed = 5;
    public  int Gap = 10;
    public int nBodyParts;

    public GameObject BodyPrefab;
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory  = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < nBodyParts; i++)
        {
            GrowSnake();
            BodyParts[i].transform.localScale = Vector3.Lerp(Vector3.one*100, Vector3.one * 25, (float)i/(float)nBodyParts);
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;

        // Set Steering
        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up *  steerDirection * SteerSpeed  * Time.deltaTime);

        if (Input.GetAxis("Vertical") != 0)
        {
            PositionsHistory.Insert(0, transform.position);
        
        int index = 1;

  
        
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionsHistory[Mathf.Min(index * Gap, PositionsHistory.Count - 1)];

            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;
            body.transform.LookAt(point);


            index++;
        }
        }

        // Remove Unneeded Positions
        if (PositionsHistory.Count > BodyParts.Count*Gap)
        {
            PositionsHistory.RemoveAt(PositionsHistory.Count - 1);
        }

        //Debug.Log(PositionsHistory.Count);

    }

    private void GrowSnake()
    {
        GameObject body = Instantiate(BodyPrefab);
        BodyParts.Add(body);

    }
}
