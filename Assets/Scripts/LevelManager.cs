using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour {
	[SerializeField] private TrackHandler[] trackPrefabs;
	[SerializeField] private CarHandler[] carPrefabs;
	[SerializeField] private PowerUpTrigger[] powerUpPrefabs;
	private void Start(){
		TrackHandler track = Instantiate(trackPrefabs[0]);
		track.SetUp(carPrefabs, powerUpPrefabs);
		track.StartRace();
	}
}