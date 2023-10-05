using System;
using UnityEngine;

public abstract class PowerUp {
	public readonly float Duration;
	protected PowerUp(float d){
		Duration = d;
	}
	public abstract string GetSpriteTag();
	public abstract void ApplyPhysicsEffectTo(CarHandler car);
	public abstract void RemovePhysicsEffectFrom(CarHandler car);
}
