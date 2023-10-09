using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class TrackHandler : MonoBehaviour {
	[SerializeField] private PowerUpSpawningConfig powerUpSpawningConfig;
	[SerializeField] private string trackName;
	[SerializeField] private int totalLaps;
	private const string TrackSectionTag = "Track Section";
	private readonly List<Vector2> trackSectionPositions = new();
	private readonly List<PowerUpTrigger> powerUpInstances = new();
	private FinishLineTrigger finishLineTrigger;
	private CarHandler[] carPrefabs;
	private CarHandler[] cars;
	private PowerUpTrigger[] powerUpPrefabs;
	private RaceEndMenu raceEndMenuPrefab;
	private PauseMenu pauseMenu;
	private Canvas pauseMenuCanvas;
	private InputAction pause;
	private InfoTextHandler infoTextHandler;
	private LoadingManager loadingManager;
	private bool isPaused;
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
	public void Initialize(CarHandler[] cps, PowerUpTrigger[] pups, RaceEndMenu remp, PauseMenu pauseMenuPrefab, InputAction p, InfoText infoTextPrefab, LoadingManager lm){
		carPrefabs = cps;
		powerUpPrefabs = pups;
		raceEndMenuPrefab = remp;
		pauseMenu = Instantiate(pauseMenuPrefab, transform);
		pauseMenu.ResumeGame.onClick.AddListener(Unpause);
		pauseMenu.RestartRace.onClick.AddListener(() => {
			RestartRace();
			Unpause();
		});
		pauseMenu.ReturnToMenu.onClick.AddListener(LoadMainMenu);
		pauseMenuCanvas = pauseMenu.GetComponent<Canvas>();
		pause = p;
		pause.Enable();
		infoTextHandler = new InfoTextHandler(Instantiate(infoTextPrefab, transform), trackName, totalLaps);
		loadingManager = lm;
		SetUpRace();
	}
	private void Pause(){
		isPaused = true;
		pauseMenuCanvas.enabled = true;
		foreach (CarHandler car in cars){
			car.RigidBody.simulated = false;
		}
	}
	private void Unpause(){
		isPaused = false;
		pauseMenuCanvas.enabled = false;
		foreach (CarHandler car in cars){
			car.RigidBody.simulated = true;
		}
	}
	private void RestartRace(){
		foreach (CarHandler car in cars){
			Destroy(car.gameObject);
		}
		foreach (PowerUpTrigger powerUp in powerUpInstances){
			Destroy(powerUp.gameObject);
		}
		SetUpRace();
		StartRace();
	}
	private void LoadMainMenu(){
		loadingManager.LoadMainMenu();
		Destroy(gameObject);
	}
	private void SetUpRace(){
		Vector2 leftmostCarPosition = finishLineTrigger.leftmostCarTransform.position;
		Vector2 rightmostCarPosition = finishLineTrigger.rightmostCarTransform.position;
		Vector2 carPos = leftmostCarPosition;
		Quaternion rotation = finishLineTrigger.transform.rotation;
		cars = new CarHandler[carPrefabs.Length];
		// Divide the distance between the two extremes by the number of gaps between cars, so for example 3 cars would have 
		// 2 gaps between cars so the difference is half of the total distance.
		Vector2 carPosDifference = cars.Length != 1 ? (rightmostCarPosition-leftmostCarPosition)/(cars.Length-1) : new Vector2();
		for (int i = 0; i < carPrefabs.Length; i++){
			cars[i] = Instantiate(carPrefabs[i], carPos, rotation, transform);
			cars[i].Initialize(this);
			carPos += carPosDifference;
		}
		currentLap = 0;
		infoTextHandler.UpdateLapCounter(currentLap);
		raceTime = 0;
		infoTextHandler.UpdateTimer(raceTime);
	}
	public void StartRace(){
		foreach (var car in cars){
			car.EnableUpdate();
		}
		raceIsOngoing = true;
		timeUntilNextSpawn = powerUpSpawningConfig.NewSpawnTime;
	}
	public void NewLap(int lap, CarHandler car){
		if (currentLap >= lap){
			return;
		}
		currentLap = lap;
		infoTextHandler.UpdateLapCounter(currentLap);
		if (currentLap >= totalLaps){
			EndRace(car);
		}
	}
	private void EndRace(CarHandler winningCar){
		raceIsOngoing = false;
		foreach (CarHandler car in cars){
			car.DisableUpdate();
			car.RemovePowerUp();
		}
		RaceEndMenu raceEndMenu = Instantiate(raceEndMenuPrefab, transform);
		raceEndMenu.Result.text = $"{winningCar.carName} won the race!";
		raceEndMenu.ReturnToMenu.onClick.AddListener(LoadMainMenu);
		raceEndMenu.PlayAgain.onClick.AddListener(() => {
			Destroy(raceEndMenu.gameObject);
			RestartRace();
		});
	}
	private void FixedUpdate(){
		if (!raceIsOngoing){
			return;
		}
		Debug.Log(pause.triggered);
		if (isPaused){0pp
			if (pause.triggered){
				Unpause();
			}
			return;
		}
		if (pause.triggered){
			Pause();
		}
		raceTime += Time.fixedDeltaTime;
		infoTextHandler.UpdateTimer(raceTime);
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
		powerUpInstances.Add(Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], spawnPosition, Quaternion.identity, transform));
	}
}