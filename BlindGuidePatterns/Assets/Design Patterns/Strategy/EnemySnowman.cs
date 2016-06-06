using UnityEngine;
using System.Collections;

public class EnemySnowman : MonoBehaviour {
	private EnemyBehaviour behaviour;
	private GameObject blindGuy;
	public GameObject projectile;
	public Sprite moltenSprite;
	private bool canSwitchAttack = false;
	private BarkPowerObserver barkObserver;

	// Use this for initialization
	void Start () {
		blindGuy = GameObject.FindWithTag("Blindguy");
		behaviour = new EnemyBehaviourPatrol(transform, 
			transform.position + transform.TransformDirection(Vector2.right) 
			* 8.0f);

		//create a new observer object and assign a callback function
		barkObserver = new BarkPowerObserver(BarkEvent);
		//add the observer to the list of subject observers for bark
		FindObjectOfType<BarkPowerSubject>().Attach(barkObserver);
	}

	void BarkEvent() {
		//attack the player when bark is triggered
		//AttackPlayer();
	}

	void OnDestroy() {
		FindObjectOfType<BarkPowerSubject>().Detach(barkObserver);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			FindObjectOfType<BarkPowerSubject>().Notify();
		}
		behaviour.Update();
		//throw projectiles at the blind guy when he's near
		if (Vector2.Distance(blindGuy.transform.position, 
			transform.position) < 10 
			&& canSwitchAttack) {
			behaviour = new EnemyBehaviourProjectile(transform, projectile);
			canSwitchAttack = false;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		//switch to a collision object when we melt
		if (coll.gameObject.tag == "FireAttack") {
			//we can no longer attack
			canSwitchAttack = false;
			GetComponent<SpriteRenderer>().sprite = moltenSprite;
			behaviour = new EnemyBehaviourStationaryCollider(
				this.gameObject, 
				EnemyBehaviourStationaryCollider.CollisionType.Ice);
			//make this a pullable object for telekinesis
			this.gameObject.tag = "PullableObject";
			GetComponent<Rigidbody2D>().isKinematic = false;
		}
	}
}
