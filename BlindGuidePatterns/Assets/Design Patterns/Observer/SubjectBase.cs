using System.Collections.Generic;
using UnityEngine;

public abstract class SubjectBase : MonoBehaviour {
	private List<ObserverBase> observers = new List<ObserverBase>();

	public void Attach(ObserverBase ob) {
		observers.Add(ob);
	}

	public void Detach(ObserverBase ob) {
		observers.Remove(ob);
	}

	public void Notify() {
		foreach (ObserverBase ob in observers) {
			ob.Update();
		}
	}
}
