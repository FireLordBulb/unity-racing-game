using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TrackHandler : MonoBehaviour {
	[SerializeField]private float powerUpMaxPositionOffset;
	private const string TrackSectionTag = "Track Section";
	private readonly List<Vector2> trackSectionPositions = new();
	private FinishLineTrigger finishLineTrigger;
	private CarHandler[] cars;
	private int currentLap;
	private Timer timer;
	private bool raceIsOngoing;
	private float raceTime;
	private float timeSinceLastSpawn;
	private void Awake(){
		foreach (Transform child in transform){
			child.TryGetComponent(out finishLineTrigger);
			if (child.CompareTag(TrackSectionTag)){
				trackSectionPositions.Add(child.transform.position);
			}
		}
		if (finishLineTrigger == null){
			throw new Exception("Track has no finish line!");
		}
	}
	public void SetUp(CarHandler[] carPrefabs, Transform carParent){
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
		timeSinceLastSpawn = 0;
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
		// TODO update UI
		timeSinceLastSpawn += Time.fixedDeltaTime;
		if (timeSinceLastSpawn > 3){
			timeSinceLastSpawn = 0;
			SpawnPowerUp();
		}
	}
	private void SpawnPowerUp(){
		int trackSectionIndex = Random.Range(0, trackSectionPositions.Count);
		Vector2 firstSectionPosition = trackSectionPositions[trackSectionIndex];
		Vector2 secondSectionPosition = trackSectionPositions[(trackSectionIndex+1)%trackSectionPositions.Count];
		Vector2 difference = secondSectionPosition-firstSectionPosition;
		// A random position on the line between the sections' positions.
		Vector2 spawnPosition = firstSectionPosition+difference*Random.value;
		// Adding a perpendicular offset from that line.
		spawnPosition += Vector2.Perpendicular(difference).normalized*Random.Range(-powerUpMaxPositionOffset, powerUpMaxPositionOffset);
		Debug.Log(spawnPosition.x+" "+spawnPosition.y);
	}
}