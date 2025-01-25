using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNodeFinder : MonoBehaviour
{
    [SerializeField]
    NodeManager nodeManager;

    [SerializeField]
    GameManager gameManager;

    Node lastNode;
    Node targetNode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Node GetTargetNode() {
        Node endNode = nodeManager.ReturnClosest(gameManager.playerObject);
        Node closestNode = nodeManager.ReturnClosest(this.gameObject);

        //fish should only get path at each node trigger maybe, while pathing to player
        List<Node> path = nodeManager.ShortestPath(closestNode, endNode);

        //go to next node
        if(path[0] != lastNode){ //if the last node visited is not the closest
            targetNode = path[0];
        } else {
            targetNode = path[1]; //go to next if closest is last visited
        }

        return targetNode;
    }
    /*
    Node GetClosestNode() {
        return nodeManager.ReturnClosest(this.gameObject);
    }
    */

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Node"))
        {
            lastNode = other.GetComponent<Node>();
        }
    }
}
