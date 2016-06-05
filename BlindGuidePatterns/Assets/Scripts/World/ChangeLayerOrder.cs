using UnityEngine;
using System.Collections;

public class ChangeLayerOrder : MonoBehaviour {
    public int orderInLayer = 0;
    public string sortingLayer = "Default";

	void Start () {
        GetComponent<Renderer>().sortingOrder = orderInLayer;
        GetComponent<Renderer>().sortingLayerName = sortingLayer;
	}
	
	void Update () {
	    
	}
}
