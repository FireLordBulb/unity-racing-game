using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour {
	[SerializeField] private TrackHandler[] trackPrefabs;
	[SerializeField] private CarHandler[] carPrefabs;
	[SerializeField] private PowerUpTrigger[] powerUpPrefabs;
	[SerializeField] private InfoText infoTextPrefab;
	private void Start(){
		TrackHandler track = Instantiate(trackPrefabs[1]);
		track.SetUp(carPrefabs, powerUpPrefabs, infoTextPrefab);
		track.StartRace();
	}
}