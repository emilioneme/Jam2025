using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    List<Node> neighbours;
    List<Vector3> positions = new List<Vector3>();
    List<float> weights = new List<float>();

    [SerializeField]
    float minDistanceForLos =2;
    
    public List<Node> GetNeighbours(){
        return neighbours;
    }

    public List<float> GetWeights(){
        return weights;
    }

    void Awake() {
        for(int i = 0; i < neighbours.Count; i++){
            positions.Add(neighbours[i].transform.position); //position of neighbour

            weights.Add(Vector3.Distance(this.transform.position, positions[i])); //dist to neighbour
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
        /*
        Debug.Log(this.gameObject.name + ": neighbour count: " + this.neighbours.Count);
        Debug.Log(this.gameObject.name + ": weights count: " + this.weights.Count);
        Debug.Log(this.gameObject.name + ": neighbours count func: " + this.GetNeighbours().Count);
        Debug.Log(this.gameObject.name + ": weights count func: " + this.GetWeights().Count);
        */
    }


    // Update is called once per frame
    void Update()
    {
        //OnDrawGizmos();
    }

    void OnDrawGizmos()
{
    Gizmos.color = Color.red;
    Gizmos.DrawSphere(this.transform.position, 1);
}

    public bool hasLosToCaller(GameObject caller){

        //Debug.Log("node los check: " + this.gameObject.name + " to " + caller.gameObject.name);
        //Debug.Log(caller.gameObject.name);

        Vector3 direction = (caller.transform.position - this.transform.position).normalized;
        //Debug.DrawRay(this.transform.position, (caller.transform.position - this.transform.position), Color.red);
        if(Physics.Raycast(transform.position, direction, out RaycastHit hitInfo)){
            if(hitInfo.transform.gameObject == caller || Vector3.Distance(this.transform.position, caller.transform.position) < minDistanceForLos){
                //Debug.Log("True for : "  + this.gameObject.name + " to " + caller.gameObject.name);
                return true;
            }
        }
        //Debug.Log("False for : "  + this.gameObject.name + " to " + caller.gameObject.name);
        //Debug.Log(hitInfo.transform.gameObject.name);
        return false;
    }
}
