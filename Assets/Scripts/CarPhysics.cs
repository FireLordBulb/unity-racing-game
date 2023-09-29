using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarPhysics : MonoBehaviour {
	[SerializeField] private PhysicsConfig physicsConfig;
	// TODO InputActionAsset
	[SerializeField] private InputAction gas, reverse, steering;
	private Vector2 gasForce, reverseForce;
	private float steerTorque;
	private Rigidbody2D rigidBody;
	private void Start(){
		gasForce = new Vector2(physicsConfig.GasForce, 0);
		reverseForce = new Vector2(physicsConfig.ReverseForce, 0);
		steerTorque = physicsConfig.SteerTorque;
		transform.localScale = physicsConfig.CarSize;
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
		rigidBody.angularDrag = physicsConfig.AngularDrag;
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
		float rotationInRadians = rigidBody.rotation*Mathf.Deg2Rad;
		Vector2 rotationVector = new Vector2(Mathf.Cos(rotationInRadians), Mathf.Sin(rotationInRadians));
		float rotationRelativeToVelocityDirection = Vector2.SignedAngle(rotationVector, rigidBody.velocity)*Mathf.Deg2Rad;
		float frictionFromAngledWheels = physicsConfig.AngledWheelsFriction*(1-Mathf.Cos(rotationRelativeToVelocityDirection));
		rigidBody.drag = physicsConfig.LinearDrag+frictionFromAngledWheels;
	}
}