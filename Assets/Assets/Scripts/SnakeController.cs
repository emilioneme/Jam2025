using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

// code inspired from https://www.youtube.com/watch?v=iuz7aUHYC_E&ab_channel=OneMinuteGames

public class SnakeController : MonoBehaviour
{
    
    public int amountOfBodyPartsAtStart = 3;
    public float bodySpeed = 1;
    public  int Gap = 10;

    public GameObject BodyPrefab;
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory  = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < amountOfBodyPartsAtStart; i++)
        {
            GrowSnake();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        PositionsHistory.Insert(0, transform.position);
        
        int index = 1;

  
        
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionsHistory[Mathf.Min(index * Gap, PositionsHistory.Count - 1)];

            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * bodySpeed * Time.deltaTime;
            body.transform.LookAt(point);


            index++;
        }
        

        // Remove Unneeded Positions
        if (PositionsHistory.Count > BodyParts.Count*Gap)
        {
            PositionsHistory.RemoveAt(PositionsHistory.Count - 1);
        }

        Debug.Log(PositionsHistory.Count);

    }

    private void GrowSnake()
    {
        GameObject body = Instantiate(BodyPrefab);
        BodyParts.Add(body);

    }
}
