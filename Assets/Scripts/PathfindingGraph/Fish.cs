using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class Fish : MonoBehaviour
{
    NodeManager nodeManager;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveToTarget() {
        Node playerNode = nodeManager.ReturnClosest(player.gameObject);
        Node closestNode = GetClosestNode();

        //fish should only get path at each node trigger maybe, while pathing to player
        //NodeManager.GetShortestPath();

    }

    Node GetClosestNode() {
        return nodeManager.ReturnClosest(this.gameObject);
    }
}
