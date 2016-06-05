using UnityEngine;
using System.Collections;
using System;

public class BlindGuyPowerupBase : BlindGuyBase {
	private BlindGuyBase blindGuy;
	public BlindGuyPowerupBase(BlindGuyBase blindGuy) {
		this.blindGuy = blindGuy;
	}

	//return the blindguy without the latest pwoer-up attached
	public BlindGuyBase Removed() {
		return blindGuy;
	}

	public override int health {
		get { return blindGuy.health; }
	}

	public override float speed {
		get { return blindGuy.speed; }
	}

	public override float viewDistance {
		get { return blindGuy.viewDistance; }
	}
}
