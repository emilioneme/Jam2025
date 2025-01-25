using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NodeManager : MonoBehaviour
{

    List<Node> allNodes;


    // Start is called before the first frame update
    void Start()
    { 
        allNodes = GetAllNodes();
        /*
        Debug.Log("after get all");
        Debug.Log(allNodes[0].gameObject.name);
        Debug.Log(allNodes[allNodes.Count - 1].gameObject.name);
        Node node0 = GameObject.Find("Node").GetComponent<Node>();
        Node node7 = GameObject.Find("Node 7").GetComponent<Node>();

        Debug.Log("shortest path between" + node0.gameObject.name + " and "+ node7.gameObject.name + " : ");
        List<Node> path = ShortestPath(node0, node7);
        for(int i = 0; i < path.Count; i++){
            Debug.Log(path[i].gameObject.name);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {

    }


    //call node manager to return closest node
    public Node ReturnClosest(GameObject caller)
    {
        Vector3 callerPos = caller.transform.position; //position of caller
        Node closestNode = null;
        foreach (Node node in allNodes)
        {
            if (closestNode == null)
            {
                closestNode = node;
            }
            else if (Vector3.Distance(callerPos, node.transform.position)
             < Vector3.Distance(callerPos, closestNode.transform.position) 
             && node.hasLosToEnemy(caller))
            {
                closestNode = node;
            }
        }

        return closestNode;
    }

    private List<Node> GetAllNodes()
    {
        List<Node> nodes = new List<Node>();
        //get all node tagged objects
        GameObject[] nodeObjects = GameObject.FindGameObjectsWithTag("Node");

        foreach (GameObject nodeObj in nodeObjects)
        {
            Node node = nodeObj.GetComponent<Node>();
            if (node != null)
            {
                nodes.Add(node);
            }
        }

        return nodes;
    }

    public List<Node> ShortestPath(Node start, Node end){
        // Dijkstra's algorithm implementation
        // Step 1: Initialize the distance and previous node dictionaries
        Dictionary<Node, float> distances = new Dictionary<Node, float>();
        Dictionary<Node, Node> previousNodes = new Dictionary<Node, Node>();
        List<Node> unvisitedNodes = new List<Node>();

        // Step 2: Initialize all nodes in the graph
        foreach (var node in GetAllNodes()) // Replace with your own node list
        {
            distances[node] = Mathf.Infinity;
            previousNodes[node] = null;
            unvisitedNodes.Add(node);
        }

        // Step 3: Set the distance to the start node to 0
        distances[start] = 0;

        // Step 4: While there are unvisited nodes
        while (unvisitedNodes.Count > 0)
        {
            // Find the node with the smallest distance
            Node currentNode = null;
            foreach (var node in unvisitedNodes)
            {
                if (currentNode == null || distances[node] < distances[currentNode])
                {
                    currentNode = node;
                }
            }

            // If the smallest distance is infinity, the target is unreachable
            if (distances[currentNode] == Mathf.Infinity)
                break;

            // Step 5: Check neighbors and update distances
            for (int i = 0; i < currentNode.GetNeighbours().Count; i++)
            {

                Node neighbor = currentNode.GetNeighbours()[i];
                float weight = currentNode.GetWeights()[i];
                float newDist = distances[currentNode] + weight;

                if (newDist < distances[neighbor])
                {
                    distances[neighbor] = newDist;
                    previousNodes[neighbor] = currentNode;
                }
            }

            // Mark the current node as visited
            unvisitedNodes.Remove(currentNode);
        }

        // Step 6: Reconstruct the shortest path
        List<Node> path = new List<Node>();
        Node current = end;
        while (current != null)
        {
            path.Insert(0, current);
            current = previousNodes[current];
        }
        
        return path;
    }

}
