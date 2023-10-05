using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarHandler : MonoBehaviour {
	[SerializeField] private PhysicsConfig physicsConfig;
	[SerializeField] private InputAction gas, reverse, steering;
	public Vector2 gasForce, reverseForce;
	public float steerTorque;
	public Rigidbody2D Rigidbody {get; private set;}
	private bool updateIsEnabled;
	private int lap;
	private PowerUp powerUp;
	private float powerUpRemainingDuration;
	private SpriteRenderer activePowerUpSprite;
	private Dictionary<string, SpriteRenderer> powerUpSprites;
	public TrackHandler currentTrack;
	private void Start(){
		gasForce = new Vector2(physicsConfig.GasForce, 0);
		reverseForce = new Vector2(physicsConfig.ReverseForce, 0);
		steerTorque = physicsConfig.SteerTorque;
		transform.localScale = physicsConfig.CarSize;
		Rigidbody = gameObject.GetComponent<Rigidbody2D>();
		Rigidbody.angularDrag = physicsConfig.AngularDrag;
		// Lap start at -1 so when the start/finishline is crossed the
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
		powerUp.ApplyPhysicsEffectTo(this);
		powerUpRemainingDuration = powerUp.Duration;
		activePowerUpSprite = powerUpSprites[powerUp.GetSpriteTag()];
		if (activePowerUpSprite != null){
			activePowerUpSprite.enabled = true;
		}
	}
	private void RemovePowerUp(){
		if (powerUp == null){
			return;
		}
		powerUp.RemovePhysicsEffectFrom(this);
		powerUp = null;
		if (activePowerUpSprite != null){
			activePowerUpSprite.enabled = false;
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
		float velocityDirectionAngle = Mathf.Atan2(Rigidbody.velocity.y, Rigidbody.velocity.x);
		float rotationRelativeToVelocityDirection = velocityDirectionAngle-Rigidbody.rotation*Mathf.Deg2Rad;
		// The absolute value of the sine is how perpendicular an angle is.
		float frictionFromAngledWheels = physicsConfig.AngledWheelsFriction*Mathf.Abs(Mathf.Sin(rotationRelativeToVelocityDirection));
		Rigidbody.drag = physicsConfig.LinearDrag+frictionFromAngledWheels;
	}
	private void ApplyInput(){
		if (gas.IsPressed()){
			Rigidbody.AddRelativeForce(gasForce);
		} else if (reverse.IsPressed()){
			Rigidbody.AddRelativeForce(reverseForce);
		}
		
		if (steering.IsPressed()){
			Rigidbody.AddTorque(steerTorque*steering.ReadValue<float>());
		}
	}
}