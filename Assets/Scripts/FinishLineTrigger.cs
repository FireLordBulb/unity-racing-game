using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineTrigger : MonoBehaviour {
	public Transform leftmostCarTransform;
	public Transform rightmostCarTransform;
	private void OnTriggerEnter2D(Collider2D other){
		ChangeLapCount(other, +1);
	}
	private void OnTriggerExit2D(Collider2D other){
		ChangeLapCount(other, -1);
	}
	// A car's lap count is only changed when the car is behind the center of the finish line, so laps only
	// increase when the car enters from behind, and they only decrease when the car exits the finish line behind
	// it. This is to prevent driving back and forth over the finish line to get laps without having driven around
	// the track.
	private void ChangeLapCount(Collider2D other, int change){
		Vector2 differenceInPositionVector = other.transform.position-this.transform.position;
		float triggerEnterAngle = Mathf.Atan2(differenceInPositionVector.y, differenceInPositionVector.x);
		float relativeEnterAngle = triggerEnterAngle-transform.eulerAngles.z*Mathf.Deg2Rad;
		// Negative cosine means negative x, which is behind since positive x is forward at 0 rotation.
		bool carIsBehindFinishLineCenter = Mathf.Cos(relativeEnterAngle) < 0;
		if (carIsBehindFinishLineCenter){
			other.GetComponent<CarHandler>().IncrementLap(change);
		}
	}
}