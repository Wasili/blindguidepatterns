using UnityEngine;
using System.Collections;
using System;

public class EnemyBehaviourStationaryCollider : EnemyBehaviour {
	public enum CollisionType { Fire, Ice, Normal }
	public EnemyBehaviourStationaryCollider(GameObject caller, CollisionType type) {
		//apply one of three different types of deaths
		switch (type) {
			case CollisionType.Fire:
				caller.AddComponent<SetFlameDeath>();
				break;
			case CollisionType.Ice:
				caller.AddComponent<SetFrozenDeath>();
				break;
			case CollisionType.Normal:
				caller.AddComponent<SetDizzyDeath>();
				break;
		}
	}

	public override void Update() {
		//collision is handled by the Set<Type>Death components
	}
}
