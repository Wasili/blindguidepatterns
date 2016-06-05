using UnityEngine;
using System.Collections;

public class EnemyBehaviourPatrol : EnemyBehaviour {
	private Transform transform;
	private Vector2 destination, start;
	private float speed = 3.0f;

	public EnemyBehaviourPatrol(Transform callerTransform, 
		Vector2 patrolDestination) {
		transform = callerTransform;
		destination = patrolDestination;
		start = transform.position;
	}

	public override void Update () {
		//move towards patrol destination
		if (Vector2.Distance(transform.position, destination) > 1.0f) {
			transform.position += ((Vector3)destination 
				- transform.position).normalized 
				* Time.deltaTime 
				* speed;
		}
		//turn around if we reach the destination
		else {
			Vector3 current = destination;
			destination = start;
			start = current;
		}
	}
}
