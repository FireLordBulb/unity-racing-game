using System;
using UnityEngine;

public class SpeedBoost : PowerUp {
	private readonly float boostedGasForce;
	private float baseGasForce;
	public SpeedBoost(float d, float b) : base(d){
		boostedGasForce = b;
	}
	public override string GetSpriteTag(){
		return "Speed Boost";
	}
	public override void ApplyEffectTo(CarHandler car){
		baseGasForce = car.GasForce.x;
		car.GasForce = new Vector2(boostedGasForce, 0);
	}
	public override void RemoveEffectFrom(CarHandler car){
		car.GasForce = new Vector2(baseGasForce, 0);
	}
}
