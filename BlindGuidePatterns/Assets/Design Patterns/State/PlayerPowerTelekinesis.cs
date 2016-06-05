using System;
using UnityEngine;

public class PlayerPowerTelekinesis : PlayerPower {
	private float pullDistance = 6.0f;
	GameObject pulledObject;

	public PlayerPowerTelekinesis(GameObject player) : base(player) {
	}

	public override void Activate() {
		//send a message to the player so we can play a sound and do a sprite switch
		player.SendMessage("SwitchPower", "Telekinesis");
	}

	public override void Deactivate() {
		//make sure the pulled object doesn't return when we switch back
		pulledObject = null;
	}

	public override void Update() {
		if (Input.GetButton("Use Power")) {
			GravitationalPull();
		}
		if (Input.GetButtonUp("Use Power")) {
			GravitationalPush();
		}
	}

	//called when power is active
	private void GravitationalPull() {
		//find all pullable objects in the scene
		GameObject[] pullableObjects = 
			GameObject.FindGameObjectsWithTag("PullableObject");

		//iterate through each object
		for (int i = 0; i < pullableObjects.Length; i++) {
			//check their distance from the player
			if (Vector3.Distance(pullableObjects[i].transform.position, 
				player.transform.position) < pullDistance) {
				pulledObject = pullableObjects[i];
				//pull object towards the player
				pullableObjects[i].GetComponent<Rigidbody2D>().velocity = 
					(player.transform.position - 
					pullableObjects[i].transform.position) 
					* 10;
			}
		}
	}

	//called when power is deactivated
	private void GravitationalPush() {
		if (pulledObject != null) {
			//throw object away, towards the mouse cursor
			pulledObject.GetComponent<Rigidbody2D>().velocity = 
				GlobalGuide.AimTowardsMouse(pulledObject.transform.position) 
				* 10;
		}
	}
}
