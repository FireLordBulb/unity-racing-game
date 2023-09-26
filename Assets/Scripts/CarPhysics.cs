using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarPhysics : MonoBehaviour {
	[SerializeField] private PhysicsConfig physicsConfig;
	[SerializeField] private InputAction gas, steering;
	private Vector2 gasForce;
	private float steerTorque;
	private Rigidbody2D rigidBody;
	private void Start(){
		gasForce = new Vector2(physicsConfig.GasForce, 0);
		steerTorque = physicsConfig.SteerTorque;
		transform.localScale = physicsConfig.CarSize;
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
		rigidBody.drag = physicsConfig.LinearDrag;
		rigidBody.angularDrag = physicsConfig.AngularDrag;
	}
	private void OnEnable(){
		gas.Enable();
		steering.Enable();
	}
	private void OnDisable(){
		gas.Enable();
		steering.Enable();
	}
	private void FixedUpdate(){
		if (gas.IsPressed()){
			rigidBody.AddRelativeForce(gasForce);
		}
		if (steering.IsPressed()){
			rigidBody.AddTorque(steerTorque*steering.ReadValue<float>());
		}
	}
}