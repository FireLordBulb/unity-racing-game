using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class PowerUpEffectConfig : ScriptableObject {
	[SerializeField] private float speedBoostDuration, speedBoostForce, iceCurseDuration, iceCurseLinearDrag, iceCurseAngledWheelsFriction;
	public float SpeedBoostDuration => speedBoostDuration;
	public float SpeedBoostForce => speedBoostForce;
	public float IceCurseDuration => iceCurseDuration;
	public float IceCurseLinearDrag => iceCurseLinearDrag;
	public float IceCurseAngledWheelsFriction => iceCurseAngledWheelsFriction;
}