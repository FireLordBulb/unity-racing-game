using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	[SerializeField] private GameObject concreteObjects;
	[SerializeField] private TrackHandler[] tracks;
	[SerializeField] private CarHandler[] cars;
	private void Start(){
		TrackHandler track = tracks[0];
		Instantiate(track.gameObject, concreteObjects.transform);
		track.SetUp(cars, concreteObjects.transform);
		track.StartRace();
	}
}