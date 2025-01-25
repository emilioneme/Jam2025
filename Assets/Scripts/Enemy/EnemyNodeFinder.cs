using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNodeFinder : MonoBehaviour
{
    [SerializeField]
    NodeManager nodeManager;

    [SerializeField]
    Player player;

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
        Node playerNode = nodeManager.ReturnClosest(player.gameObject);
        Node closestNode = GetClosestNode();

        //fish should only get path at each node trigger maybe, while pathing to player
        List<Node> path = nodeManager.ShortestPath(closestNode, playerNode);

        //go to next node
        if(path[0] != lastNode){ //if the last node visited is not the closest
            targetNode = path[0];
        } else {
            targetNode = path[1]; //go to next if closest is last visited
        }

        return targetNode;
    }

    Node GetClosestNode() {
        return nodeManager.ReturnClosest(this.gameObject);
    }
}
