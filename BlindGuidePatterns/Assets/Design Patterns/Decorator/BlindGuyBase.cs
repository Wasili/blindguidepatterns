using UnityEngine;
using System.Collections;

public abstract class BlindGuyBase {
	public abstract int health { get; }
	public abstract float viewDistance { get; }
	public abstract float speed { get; }
}
