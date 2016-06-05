using UnityEngine;
using System.Collections;
using System;

public class EnemyBehaviourProjectile : EnemyBehaviour {
	private Transform transform;
	private GameObject projectile, projectilePrefab;
	private Vector2 target;
	private float speed = 15.0f;
	private float timer;

	public EnemyBehaviourProjectile(Transform callerTransform, 
		GameObject projectileToThrow) {
		transform = callerTransform;
		projectilePrefab = projectileToThrow;
		Throw();
	}

	public override void Update() {
		//throw a projectile every couple of seconds
		timer -= Time.deltaTime;
		if (timer <= 0) {
			Throw();
		}
	}

	private void Throw() {
		timer = 5.0f;
		projectile = (GameObject)GameObject.Instantiate(projectilePrefab,
			transform.position,
			transform.rotation);
		target = GameObject.FindWithTag("Blindguy").transform.position;
	}
}
