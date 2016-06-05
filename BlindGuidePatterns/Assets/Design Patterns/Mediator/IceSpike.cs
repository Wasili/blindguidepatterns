using UnityEngine;
using System.Collections;
using System;

public class IceSpike : AchievementObject {
	public override void AnimateAchievement() {
		//instantiate the object containing the animation to be played
		Instantiate(animationPrefab, transform.position, transform.rotation);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		//this object can be destroyed by fire
		if (coll.gameObject.tag == "FireAttack") {
			//see if we should play an animation upon destruction
			FindObjectOfType<Mediator>()
				.ObjectDestroyedAchievementcheck(this);
		}
	}
}
