using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineTrigger : MonoBehaviour {
	private void Start(){}
	private void OnTriggerEnter2D(Collider2D other){
		Vector2 difference = other.transform.position-transform.position;
		if (Vector2.SignedAngle(Vector2.right, difference) < 0){
			Debug.Log("lap!");
			// TODO reference to race handler
		}
	}
}