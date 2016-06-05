using UnityEngine;
using System.Collections;
using System;

public class BlindGuyStats : BlindGuyBase {
	public override int health {
		get { return 0; }
	}

	public override float speed {
		get { return 2.0f; }
	}

	public override float viewDistance {
		get { return 5.5f; }
	}
}
