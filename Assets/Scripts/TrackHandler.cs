using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TrackHandler : MonoBehaviour {

	[SerializeField]private PowerUpSpawningConfig powerUpSpawningConfig;
	private const string TrackSectionTag = "Track Section";
	private readonly List<Vector2> trackSectionPositions = new();
	private readonly List<PowerUpTrigger> powerUpInstances = new();
	private FinishLineTrigger finishLineTrigger;
	private CarHandler[] cars;
	private PowerUpTrigger[] powerUps;
	private int currentLap;
	private Timer timer;
	private bool raceIsOngoing;
	private float raceTime;
	private float timeUntilNextSpawn;
	private void Awake(){
		foreach (Transform child in transform){
			if (finishLineTrigger == null && child.TryGetComponent(out FinishLineTrigger trigger)){
				finishLineTrigger = trigger;
			}
			if (child.CompareTag(TrackSectionTag)){
				trackSectionPositions.Add(child.transform.position);
			}
		}
		if (finishLineTrigger == null){
			Destroy(gameObject);
			throw new Exception("Track has no finish line!");
		}
	}
	public void SetUp(CarHandler[] carPrefabs, Transform carParent, PowerUpTrigger[] powerUpPrefabs){
		powerUps = powerUpPrefabs;
		Vector2 leftmostCarPosition = finishLineTrigger.leftmostCarTransform.position;
		Vector2 rightmostCarPosition = finishLineTrigger.rightmostCarTransform.position;
		Vector2 carPos = leftmostCarPosition;
		Quaternion rotation = finishLineTrigger.transform.rotation;
		cars = new CarHandler[carPrefabs.Length];
		// Divide the distance between the two extremes by the number of gaps between cars, so for example 3 cars would have 
		// 2 gaps between cars so the difference is half of the total distance.
		Vector2 carPosDifference = cars.Length != 1 ? (rightmostCarPosition-leftmostCarPosition)/(cars.Length-1) : new Vector2();
		for (int i = 0; i < carPrefabs.Length; i++){
			cars[i] = Instantiate(carPrefabs[i], carPos, rotation, carParent);
			cars[i].currentTrack = this;
			carPos += carPosDifference;
		}
		currentLap = 0;
	}
	public void StartRace(){
		foreach (var car in cars){
			car.EnableUpdate();
		}
		raceIsOngoing = true;
		raceTime = 0;
		timeUntilNextSpawn = powerUpSpawningConfig.NewSpawnTime;
	}
	public void NewLap(int lap){
		if (currentLap < lap){
			currentLap = lap;
		}
		// TODO update UI
		Debug.Log(currentLap);
		// TODO end race if last lap. 
	}
	private void FixedUpdate(){
		if (!raceIsOngoing){
			return;
		}
		raceTime += Time.fixedDeltaTime;
		HandlePowerUps();
	}
	private void HandlePowerUps(){
		// References to destroyed objects become null references and have to be manually removed.
		powerUpInstances.RemoveAll(powerUp => powerUp == null);
		if (powerUpInstances.Count >= powerUpSpawningConfig.MaxInstances){
			return;
		}
		timeUntilNextSpawn -= Time.fixedDeltaTime;
		if (timeUntilNextSpawn < 0){
			timeUntilNextSpawn = powerUpSpawningConfig.NewSpawnTime;
			SpawnPowerUp();
		}
	}
	// Spawns a random power-up on a random point of the track, within some offset from the center line of the track. 
	private void SpawnPowerUp(){
		int trackSectionIndex = Random.Range(0, trackSectionPositions.Count);
		Vector2 firstSectionPosition = trackSectionPositions[trackSectionIndex];
		Vector2 secondSectionPosition = trackSectionPositions[(trackSectionIndex+1)%trackSectionPositions.Count];
		// There's no point in using Lerp since I need the difference Vector to get the perpendicular Vector anyway.
		Vector2 difference = secondSectionPosition-firstSectionPosition;
		// A random position on the center line of the track.
		Vector2 spawnPosition = firstSectionPosition+difference*Random.value;
		// Adding a perpendicular offset from that line.
		spawnPosition += Vector2.Perpendicular(difference).normalized*powerUpSpawningConfig.NewPositionOffset;
		powerUpInstances.Add(Instantiate(powerUps[Random.Range(0, powerUps.Length)], spawnPosition, Quaternion.identity));
	}
}