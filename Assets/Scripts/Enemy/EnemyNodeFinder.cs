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

    /*
        public Node GetTargetNode() {
            Node endNode = nodeManager.ReturnClosest(gameManager.playerObject);
            Node closestNode = nodeManager.ReturnClosest(this.gameObject);

            //fish should only get path at each node trigger maybe, while pathing to player
            List<Node> path = nodeManager.ShortestPath(closestNode, endNode);

            for(int i = 0; i < path.Count; i++){
                Debug.Log("Path "+ i + ": " + path[i].gameObject.name);
            }

            //go to next node
            if(path[0] != lastNode || path.Count == 1){ //if the last node visited is not the closest
                targetNode = path[0];
            } else {
                targetNode = path[1]; //go to next if closest is last visited
            }

            return targetNode;
        }
        */
    public List<Node> GetTargetPath()
    {
        Node endNode = nodeManager.ReturnClosest(gameManager.playerObject);
        Node closestNode = nodeManager.ReturnClosest(this.gameObject);

        //Debug.Log("DEBUG #### CLOSEST RETURNED: " + closestNode.gameObject.name);
        ///////////WORKING START///////////
        /*
        //fish should only get path at each node trigger maybe, while pathing to player
        List<Node> path = nodeManager.ShortestPath(closestNode, endNode);

        
        for(int i = 0; i < path.Count; i++){
            Debug.Log("Path "+ i + ": " + path[i].gameObject.name);
        }
        

        //go to next node
        if(path[0] != lastNode || path.Count == 1){ //if the last node visited is not the closest
            targetNode = path[0];
        } else {
            path.RemoveAt(0); //go to next if closest is last visited
        }

        return path;
        */
        ////////////////WORKING END///////////


        //fish should only get path at each node trigger maybe, while pathing to player
        if (closestNode != null && endNode != null)
        {
            List<Node> path = nodeManager.ShortestPath(closestNode, endNode);
            //for (int i = 0; i < path.Count; i++)
            // {
            //Debug.Log("Path " + i + ": " + path[i].gameObject.name);
            // }


            //go to next node
            if (path[0] != lastNode || path.Count == 1)
            { //if the last node visited is not the closest
                targetNode = path[0];
            }
            else
            {
                path.RemoveAt(0); //go to next if closest is last visited
            }

            return path;
        }
        else
        {
            List<Node> path = new List<Node>();

            Vector3 direction = (lastNode.transform.position - gameManager.enemyObject.transform.position).normalized;

            if (Physics.Raycast(gameManager.enemyObject.transform.position, direction, out RaycastHit hitInfo))
            {
                if (hitInfo.transform.gameObject != lastNode)
                {
                    gameManager.enemyObject.GetComponent<EnemyMovement>().directionFlip = -1;
                }
            }
            path.Add(lastNode);
            return path;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Node"))
        {
            lastNode = other.GetComponent<Node>();
            //Debug.Log("last node : " + lastNode.gameObject.name);

            gameManager.enemyObject.GetComponent<EnemyMovement>().NextNode();

        }
    }
}
