using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class PhysicsConfig : ScriptableObject {
	[SerializeField] private float linearDrag, angledWheelsFriction, angularDrag, gasForce, reverseForce, steerTorque;
	public float LinearDrag => linearDrag;
	public float AngledWheelsFriction => angledWheelsFriction;
	public float AngularDrag => angularDrag;
	public float GasForce => gasForce;
	public float ReverseForce => reverseForce;
	public float SteerTorque => steerTorque;
}