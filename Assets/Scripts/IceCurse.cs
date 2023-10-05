using System;
using UnityEngine;

public class IceCurse : PowerUp {
	private readonly float iceLinearDrag;
	private readonly float iceAngledWheelsFriction;
	private float baseLinearDrag;
	private float baseAngledWheelsFriction;
	public IceCurse(float d, float ld, float awf) : base(d){
		iceLinearDrag = ld;
		iceAngledWheelsFriction = awf;
	}
	public override string GetSpriteTag(){
		return "Ice Curse";
	}
	public override void ApplyEffectTo(CarHandler car){
		baseLinearDrag = car.LinearDrag;
		baseAngledWheelsFriction = car.AngledWheelsFriction;
		car.LinearDrag = iceLinearDrag;
		car.AngledWheelsFriction = iceAngledWheelsFriction;
	}
	public override void RemoveEffectFrom(CarHandler car){
		car.LinearDrag = baseLinearDrag;
		car.AngledWheelsFriction = baseAngledWheelsFriction;
	}
}
