using UnityEngine;
using System.Collections;

public class BlindGuyHandler : MonoBehaviour {
	BlindGuyBase blindGuy;
	BlindGuyPowerupBase powerup;
	BlindGuyAI blindGuyAI;
	
	void Start () {
		//create a new blind guy stats object
		blindGuy = new BlindGuyStats();
		//give the powerup a base to work with
		powerup = new BlindGuyPowerupBase(blindGuy);
		blindGuyAI = GetComponent<BlindGuyAI>();
	}
	
	public void AddHealth() {
		/* apply a new power-up and give the current 
		 * power-up as base to expand upon */
		powerup = new BlindGuyPowerupHealth(powerup);
	}

	public void AddViewDistance() {
		powerup = new BlindGuyPowerupViewDistance(powerup);
	}

	public void AddSpeed() {
		powerup = new BlindGuyPowerupSpeed(powerup);
	}

	public void RemovePowerup() {
		powerup = (BlindGuyPowerupBase)powerup.Removed();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			AddViewDistance();
		}
		if (Input.GetKeyDown(KeyCode.O)) {
			RemovePowerup();
		}

		//update blind guy values
		blindGuyAI.health = powerup.health;
		blindGuyAI.viewDistance = powerup.viewDistance;
		blindGuyAI.speed = powerup.speed;
	}
}
