using UnityEngine;
using System.Collections;

public class LavaFall : MonoBehaviour {
	private EnemyBehaviour behaviour;
	public Sprite frozenSprite;

	void Start () {
		behaviour = new EnemyBehaviourStationaryCollider(
			this.gameObject, 
			EnemyBehaviourStationaryCollider.CollisionType.Fire);
	}

	void Update() {
		behaviour.Update();
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "IceAttack") {
			//we don't want to destroy the object
			behaviour = new EnemyBehaviourNull();
			//we simply want it to no be harmful and change its sprite
			GetComponent<SpriteRenderer>().sprite = frozenSprite;
		}
	}
}
