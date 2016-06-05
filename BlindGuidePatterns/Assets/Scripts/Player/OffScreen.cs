using UnityEngine;
using System.Collections;

public class OffScreen : MonoBehaviour {
	void Update () 
	{
		Vector3 playerSize = GetComponent<Renderer>().bounds.size;
		
		// Here is the definition of the boundary in world point
		float distance = (transform.position - Camera.main.transform.position).z;

        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + (playerSize.x / 2);
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - (playerSize.x / 2);

        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).y + (playerSize.y / 2);
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance)).y - (playerSize.y / 2);
		
		// Here the position of the player is clamped into the boundaries
		transform.position = (new Vector3 (
			Mathf.Clamp (transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp (transform.position.y, bottomBorder, topBorder),
			transform.position.z));
	}
}
