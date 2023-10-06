using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarHandler : MonoBehaviour {
	[SerializeField] private PhysicsConfig physicsConfig;
	[SerializeField] private InputAction gas, reverse, steering;
	public Vector2 GasForce {get; set;}
	private Vector2	ReverseForce {get; set;}
	private float SteerTorque {get; set;}
	public float LinearDrag {get; set;}
	public float AngledWheelsFriction {get; set;}
	public Rigidbody2D RigidBody {get; private set;}
	private bool updateIsEnabled;
	private TrackHandler currentTrack;
	private int lap;
	private PowerUp powerUp;
	private float powerUpRemainingDuration;
	private SpriteRenderer activePowerUpSprite;
	private Dictionary<string, SpriteRenderer> powerUpSprites;
	
	public void Initialize(TrackHandler track){
		RigidBody = gameObject.GetComponent<Rigidbody2D>();
		currentTrack = track;
	}
	private void Start(){
		GasForce = new Vector2(physicsConfig.GasForce, 0);
		ReverseForce = new Vector2(physicsConfig.ReverseForce, 0);
		SteerTorque = physicsConfig.SteerTorque;
		LinearDrag = physicsConfig.LinearDrag;
		AngledWheelsFriction = physicsConfig.AngledWheelsFriction;
		RigidBody.angularDrag = physicsConfig.AngularDrag;
		// Lap starts at -1 so when the start/finishline is crossed the
		// first time, starting line one, the car is on lap 0.
		lap = -1;
		powerUp = null;
		activePowerUpSprite = null;
		powerUpSprites = new Dictionary<string, SpriteRenderer>();
		foreach (Transform child in transform){
			if (!child.TryGetComponent(out SpriteRenderer sprite)){
				continue;
			}
			powerUpSprites.Add(child.tag, sprite);
			sprite.enabled = false;
		}
	}
	public void IncrementLap(int change){
		lap += change;
		currentTrack.NewLap(lap);
	}
	public void GivePowerUp(PowerUp p){
		RemovePowerUp();
		powerUp = p;
		powerUp.ApplyEffectTo(this);
		powerUpRemainingDuration = powerUp.Duration;
		activePowerUpSprite = powerUpSprites[powerUp.GetSpriteTag()];
		if (activePowerUpSprite != null){
			activePowerUpSprite.enabled = true;
		}
	}
	public void RemovePowerUp(){
		if (powerUp == null){
			return;
		}
		powerUp.RemoveEffectFrom(this);
		powerUp = null;
		if (activePowerUpSprite != null){
			activePowerUpSprite.enabled = false;
			activePowerUpSprite = null;
		}
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
		UpdatePowerUp();
		UpdateDrag();
		ApplyInput();
	}
	private void UpdatePowerUp(){
		if (powerUp == null){
			return;
		}
		powerUpRemainingDuration -= Time.fixedDeltaTime;
		if (powerUpRemainingDuration < 0){
			RemovePowerUp();
		}
	}
	private void UpdateDrag(){
		float velocityDirectionAngle = Mathf.Atan2(RigidBody.velocity.y, RigidBody.velocity.x);
		float rotationRelativeToVelocityDirection = velocityDirectionAngle-RigidBody.rotation*Mathf.Deg2Rad;
		// The absolute value of the sine is how perpendicular an angle is.
		float frictionFromAngledWheels = AngledWheelsFriction*Mathf.Abs(Mathf.Sin(rotationRelativeToVelocityDirection));
		RigidBody.drag = LinearDrag+frictionFromAngledWheels;
	}
	private void ApplyInput(){
		if (gas.IsPressed()){
			RigidBody.AddRelativeForce(GasForce);
		} else if (reverse.IsPressed()){
			RigidBody.AddRelativeForce(ReverseForce);
		}
		
		if (steering.IsPressed()){
			RigidBody.AddTorque(SteerTorque*steering.ReadValue<float>());
		}
	}
}