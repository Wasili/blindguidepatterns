using UnityEngine;
using System.Collections;

public class BlindGuyPowerupSpeed : BlindGuyPowerupBase {
	public BlindGuyPowerupSpeed(BlindGuyBase blindGuy) : base(blindGuy) { }
	public override float speed {
		get { return base.speed + 0.5f; }
	}

	public override int health {
		get { return base.health + 1; }
	}
}
