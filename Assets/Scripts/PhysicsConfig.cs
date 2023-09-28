using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class PhysicsConfig : ScriptableObject {
	[SerializeField] private Vector2 carSize;
	[SerializeField] private float linearDrag, angularDrag, gasForce, reverseForce, steerTorque;
	public Vector2 CarSize => carSize;
	public float LinearDrag => linearDrag;
	public float AngularDrag => angularDrag;
	public float GasForce => gasForce;
	public float ReverseForce => reverseForce;
	public float SteerTorque => steerTorque;
}