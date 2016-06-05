using UnityEngine;
using System.Collections;

public class ParalaxBackground : MonoBehaviour {

    public Transform[] backgrounds;
    public float[] paralaxSpeeds;
	
	void Update () 
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].transform.position += new Vector3(GameObject.FindWithTag("Blindguy").GetComponent<BlindGuyAI>().speed * paralaxSpeeds[i] * Time.deltaTime, 0, 0);
        }
	}
}
