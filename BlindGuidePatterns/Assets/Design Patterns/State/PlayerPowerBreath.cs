using UnityEngine;
using System.Collections;
using System;

public class PlayerPowerBreath : PlayerPower {
	private GameObject powerObject, powerPrefab;

	public PlayerPowerBreath(GameObject player, GameObject icePowerPrefab) 
		: base(player) {
		this.powerPrefab = icePowerPrefab;
	}

	public override void Activate() {
		//trigger spreite switches, animations and sounds
		player.SendMessage("SwitchPower", "Ice");
	}

	public override void Deactivate() {
		//destroy the spawned power when we switch
		GameObject.Destroy(powerObject);
	}

	public override void Update() {
		if (Input.GetButtonDown("Use Power")) {
			//spawn the power object
			powerObject = (GameObject)GameObject.Instantiate(powerPrefab, 
				player.transform.position, 
				player.transform.rotation);
			//make sure it follows the player position and rotation
			powerObject.transform.parent = player.transform;
			//place it slightly in front of the player
			powerObject.transform.localPosition = new Vector2(-2.0f, 0.0f);
		}
	}
}
