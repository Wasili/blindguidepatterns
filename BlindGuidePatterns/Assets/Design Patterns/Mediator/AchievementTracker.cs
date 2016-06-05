using UnityEngine;
using System.Collections;

public class AchievementTracker : MonoBehaviour {
	//keep track of how many obstacles have been destroyed so far
	static int objDestructionCount = 0;
	bool objDestructionCountUnlocked = false;

	//update the amount of obstacles detroyed so far
	public void UpdateObjectDestructionTracker(AchievementObject obj) {
		objDestructionCount++;
		if (objDestructionCount > 10 && !objDestructionCountUnlocked) {
			//play an animation sequence on the object we destroyed
			FindObjectOfType<Mediator>().AnimateAchievementUnlock(obj);
			objDestructionCountUnlocked = true;
		}
	}

	//reset the destruction tracker
	public void ResetObjectDestructionTracker() {
		objDestructionCount = 0;
	}
}
