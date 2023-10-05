using System;
using UnityEngine;

[Serializable]
public class SpeedBoost : PowerUp {
	private readonly float boostedGasForce;
	private float baseGasForce;
	public SpeedBoost(float d, float b) : base(d){
		boostedGasForce = b;
	}
	public override string GetSpriteTag(){
		return "Speed Boost";
	}
	public override void ApplyPhysicsEffectTo(CarHandler car){
		baseGasForce = car.gasForce.x;
		car.gasForce.x = boostedGasForce;
		car.Rigidbody.position += new Vector2();
	}
	public override void RemovePhysicsEffectFrom(CarHandler car){
		car.gasForce.x = baseGasForce;
	}
}
