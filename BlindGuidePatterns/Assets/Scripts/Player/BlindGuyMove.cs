using UnityEngine;
using System.Collections;

public class BlindGuyMove : MonoBehaviour {

    public float speed;
	
	void Update () {
        this.gameObject.transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
	
	}
}
