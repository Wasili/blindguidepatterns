using UnityEngine;
using System.Collections;
using System;

public class BarkPowerObserver : ObserverBase {
	//create a delegate for callbacks
	public delegate void BarkCallback();
	//callback function is stored in this delegate
	private BarkCallback barkCallback;

	public BarkPowerObserver(BarkCallback callbackFunction) {
		//save the callback method
		barkCallback = callbackFunction;
	}

	public override void Update() {
		//execute the callback method
		barkCallback();
	}
}
