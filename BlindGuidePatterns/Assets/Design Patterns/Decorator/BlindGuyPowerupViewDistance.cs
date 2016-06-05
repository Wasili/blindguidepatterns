using UnityEngine;
using System.Collections;

public class BlindGuyPowerupViewDistance : BlindGuyPowerupBase {
	public BlindGuyPowerupViewDistance(BlindGuyBase blindGuy) : base(blindGuy) { }
	public override float viewDistance {
		get { return base.viewDistance + 0.5f; }
	}

	public override int health {
		get { return base.health + 1; }
	}
}
