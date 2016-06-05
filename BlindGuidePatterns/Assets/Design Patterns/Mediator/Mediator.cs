using UnityEngine;
using System.Collections;

public class Mediator : MonoBehaviour {
	public void ObjectDestroyedAchievementcheck(AchievementObject obj) {
		//update the achievement tracker
		FindObjectOfType<AchievementTracker>().UpdateObjectDestructionTracker(obj);
	}

	public void AnimateAchievementUnlock(AchievementObject obj) {
		//polay an animation because an achievement was unlocked
		obj.AnimateAchievement();
	}

	public void ResetAchievementObjectTracker() {
		FindObjectOfType<AchievementTracker>().ResetObjectDestructionTracker();
	}
}
