using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarHandler : MonoBehaviour {
	[SerializeField] private PhysicsConfig physicsConfig;
	[SerializeField] private InputAction gas, reverse, steering;
	private Vector2 gasForce, reverseForce;
	private float steerTorque;
	private Rigidbody2D rigidBody;
	private bool updateIsEnabled;
	private int lap;
	public TrackHandler currentTrack;
	private void Start(){
		gasForce = new Vector2(physicsConfig.GasForce, 0);
		reverseForce = new Vector2(physicsConfig.ReverseForce, 0);
		steerTorque = physicsConfig.SteerTorque;
		transform.localScale = physicsConfig.CarSize;
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
		rigidBody.angularDrag = physicsConfig.AngularDrag;
		// Lap start at -1 so when the start/finishline is crossed the
		// first time, starting line one, the car is on lap 0.
		lap = -1;
	}
	public void IncrementLap(int change){
		lap += change;
		currentTrack.NewLap(lap);
	}
	public void EnableUpdate(){
		updateIsEnabled = true;
	}
	public void DisableUpdate(){
		updateIsEnabled = false;
	}
	private void OnEnable(){
		gas.Enable();
		reverse.Enable();
		steering.Enable();
	}
	private void OnDisable(){
		gas.Disable();
		reverse.Disable();
		steering.Disable();
	}
	private void FixedUpdate(){
		if (!updateIsEnabled){
			return;
		}
		UpdateDrag();
		if (gas.IsPressed()){
			rigidBody.AddRelativeForce(gasForce);
		} else if (reverse.IsPressed()){
			rigidBody.AddRelativeForce(reverseForce);
		}
		
		if (steering.IsPressed()){
			rigidBody.AddTorque(steerTorque*steering.ReadValue<float>());
		}
	}
	private void UpdateDrag(){
		float velocityDirectionAngle = Mathf.Atan2(rigidBody.velocity.y, rigidBody.velocity.x);
		float rotationRelativeToVelocityDirection = velocityDirectionAngle-rigidBody.rotation*Mathf.Deg2Rad;
		// The absolute value of the sine is how perpendicular an angle is.
		float frictionFromAngledWheels = physicsConfig.AngledWheelsFriction*Mathf.Abs(Mathf.Sin(rotationRelativeToVelocityDirection));
		rigidBody.drag = physicsConfig.LinearDrag+frictionFromAngledWheels;
	}
}