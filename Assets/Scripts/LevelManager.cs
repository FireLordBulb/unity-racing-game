using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour {
	[SerializeField] private GameObject concreteObjects;
	[FormerlySerializedAs("tracks")] [SerializeField] private TrackHandler[] trackPrefabs;
	[FormerlySerializedAs("cars")] [SerializeField] private CarHandler[] carPrefabs;
	private void Start(){
		TrackHandler track = Instantiate(trackPrefabs[0], concreteObjects.transform);
		track.SetUp(carPrefabs, concreteObjects.transform);
		track.StartRace();
	}
}