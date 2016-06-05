using UnityEngine;
using System.Collections;
using System;

public class EnemyBehaviourCharge : EnemyBehaviour {
	private Transform transform;
	private Vector2 velocity;
	private float speed = 10.0f;

	public EnemyBehaviourCharge(Transform callerTransform, Vector2 chargeDestination) {
		transform = callerTransform;
		//calculate velocity once based on destination
		velocity = (chargeDestination - (Vector2)transform.position).normalized;
	}

	public override void Update() {
		//move with charge velocity
		transform.position += (Vector3)velocity
			* Time.deltaTime
			* speed;
	}
}
