using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineTrigger : MonoBehaviour {
	public Transform leftmostCarTransform;
	public Transform rightmostCarTransform;
	private void OnTriggerEnter2D(Collider2D other){
		IncrementLap(other, +1);
	}
	private void OnTriggerExit2D(Collider2D other){
		IncrementLap(other, -1);
	}
	private void IncrementLap(Collider2D other, int change){
		Vector2 differenceInPositionVector = other.transform.position-this.transform.position;
		float triggerEnterAngle = Mathf.Atan2(differenceInPositionVector.y, differenceInPositionVector.x);
		float relativeEnterAngle = triggerEnterAngle-transform.eulerAngles.z*Mathf.Deg2Rad;
		// Negative cosine means negative x, which is behind since positive x is forward at 0 rotation.
		bool carCrossedFromBehind = Mathf.Cos(relativeEnterAngle) < 0;
		if (carCrossedFromBehind){
			other.GetComponent<CarHandler>().IncrementLap(change);
		}
	}
}