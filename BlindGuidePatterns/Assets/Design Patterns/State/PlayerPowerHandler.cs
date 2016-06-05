using UnityEngine;
using System.Collections;

public class PlayerPowerHandler : MonoBehaviour {
	PlayerPower[] powers;
	int currentPower = 0;
	public GameObject icePowerPrefab, firePowerPrefab;

	void Start() {
		//set the available powers
		 powers = new PlayerPower[] {
			 new PlayerPowerTelekinesis(this.gameObject),
			 new PlayerPowerBreath(this.gameObject, firePowerPrefab),
			 new PlayerPowerBreath(this.gameObject, icePowerPrefab) };
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetButtonDown("Switch Power")) {
			//deactivate current power
			powers[currentPower].Deactivate();
			//go to next power if available, otherwise pick the first
			currentPower = currentPower < powers.Length-1 ?currentPower+1 :0;
			//actiate the new power
			powers[currentPower].Activate();
		}

		powers[currentPower].Update();
	}
}
