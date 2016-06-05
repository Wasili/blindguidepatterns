using UnityEngine;
using System.Collections;

public abstract class PlayerPower {
	//keep track of player object
	protected GameObject player;
	public PlayerPower(GameObject player) {
		this.player = player;
	}
	//frame-by-frame logic
	public abstract void Update();
	//call when power is selected
	public abstract void Activate();
	//call when power is deselected
	public abstract void Deactivate();
}
