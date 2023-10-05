using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour {
	[SerializeField] private GameObject concreteObjects;
	[SerializeField] private TrackHandler[] trackPrefabs;
	[SerializeField] private CarHandler[] carPrefabs;
	[SerializeField] private PowerUpTrigger[] powerUpPrefabs;
	private void Start(){
		TrackHandler track = Instantiate(trackPrefabs[0], concreteObjects.transform);
		track.SetUp(carPrefabs, concreteObjects.transform, powerUpPrefabs);
		track.StartRace();
	}
}