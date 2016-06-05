using UnityEngine;
using System.Collections;

public class SetParticleSortingLayer : MonoBehaviour {

	void Start () {

        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "ParticleLayer";

	}
}
