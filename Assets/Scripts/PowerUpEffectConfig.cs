using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class PowerUpEffectConfig : ScriptableObject {
	[SerializeField] private float speedBoostDuration, speedBoostForce;
	public float SpeedBoostDuration => speedBoostDuration;
	public float SpeedBoostForce => speedBoostForce;
}