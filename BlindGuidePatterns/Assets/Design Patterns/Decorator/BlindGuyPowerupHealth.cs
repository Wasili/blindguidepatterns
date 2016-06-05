using UnityEngine;
using System.Collections;

public class BlindGuyPowerupHealth : BlindGuyPowerupBase {
	public BlindGuyPowerupHealth(BlindGuyBase blindGuy) : base(blindGuy) { }
	public override int health {
		get { return base.health + 1; }
	}
}
