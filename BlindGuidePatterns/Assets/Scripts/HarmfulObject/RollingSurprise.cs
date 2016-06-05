using UnityEngine;
using System.Collections;

public class RollingSurprise : MonoBehaviour {
    public GameObject rollingIceStone;
    public float shootSpeed, shootDistance;
    bool attack = true;

	void Start () {
	
	}
	
	void Update () {

        if (Vector2.Distance(transform.position, GameObject.FindWithTag("Blindguy").transform.position) <= shootDistance && attack == true)
        {
            GameObject Surprise = (GameObject)Instantiate(rollingIceStone, new Vector3(transform.position.x-5, transform.position.y, 0),
                      Quaternion.Euler(0, 0, 0));
            Surprise.GetComponent<Rollingstones>().speed = shootSpeed;
            attack = false;
        }
	}
}
