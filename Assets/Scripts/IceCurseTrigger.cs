using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IceCurseTrigger : PowerUpTrigger {
	private void Awake(){
		PowerUp = new IceCurse(powerUpConfig.IceCurseDuration, powerUpConfig.IceCurseLinearDrag, powerUpConfig.IceCurseAngledWheelsFriction);
	}
}