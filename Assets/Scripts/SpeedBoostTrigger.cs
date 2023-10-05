using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpeedBoostTrigger : PowerUpTrigger {
	private void Awake(){
		PowerUp = new SpeedBoost(powerUpConfig.SpeedBoostDuration, powerUpConfig.SpeedBoostForce);
	}
}