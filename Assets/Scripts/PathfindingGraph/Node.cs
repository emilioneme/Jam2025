using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    List<Node> neighbours;
    List<Vector3> positions;
    List<float> weights;
    
    public List<Node> GetNeighbours(){
        return neighbours;
    }

    public List<float> GetWeights(){
        return weights;
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < neighbours.Count; i++){
            positions[i] = neighbours[i].transform.position; //position of neighbour

            weights[i] = Vector3.Distance(this.transform.position, positions[i]); //dist to neighbour
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
