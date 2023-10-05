using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class PowerUpTrigger : MonoBehaviour {
	[SerializeField]protected PowerUpEffectConfig powerUpConfig;
	protected PowerUp PowerUp;
	private void OnTriggerEnter2D(Collider2D other){
		other.GetComponent<CarHandler>().GivePowerUp(PowerUp);
		Destroy(gameObject);
	}
}