using UnityEngine;
using System.Collections;

public abstract class AchievementObject : MonoBehaviour {
	//the prefab containing the animation to be played
	public GameObject animationPrefab;
	//trigger the animation sequence for an unlocked achievement
	public abstract void AnimateAchievement();
}
