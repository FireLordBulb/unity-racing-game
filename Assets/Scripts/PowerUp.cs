using System;
using UnityEngine;

public abstract class PowerUp {
	public readonly float Duration;
	protected PowerUp(float d){
		Duration = d;
	}
	public abstract string GetSpriteTag();
	public abstract void ApplyEffectTo(CarHandler car);
	public abstract void RemoveEffectFrom(CarHandler car);
}
