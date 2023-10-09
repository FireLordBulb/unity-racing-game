using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour {
	[SerializeField] private TrackHandler[] trackPrefabs;
	private TrackHandler currentTrack;
	[SerializeField] private CarHandler[] carPrefabs;
	[SerializeField] private PowerUpTrigger[] powerUpPrefabs;
	[SerializeField] private MainMenu mainMenuPrefab;
	[SerializeField] private RaceEndMenu raceEndMenuPrefab;
	[SerializeField] private PauseMenu pauseMenuPrefab;
	[SerializeField] private InputAction pause;
	[SerializeField] private InfoText infoTextPrefab;
	private void Awake(){
		LoadMainMenu();
	}
	public void LoadMainMenu(){
		MainMenu mainMenu = Instantiate(mainMenuPrefab);
		for (int i = 0; i < trackPrefabs.Length && i < mainMenu.LevelButtons.Length; i++){
			TrackHandler trackPrefab = trackPrefabs[i];
			mainMenu.LevelButtons[i].onClick.AddListener(() => {
				LoadTrack(trackPrefab);
				Destroy(mainMenu.gameObject);
			});
		}
	}
	private void LoadTrack(TrackHandler trackPrefab){
		if (currentTrack != null){
			Destroy(currentTrack.gameObject);
		}
		currentTrack = Instantiate(trackPrefab);
		currentTrack.Initialize(carPrefabs, powerUpPrefabs, raceEndMenuPrefab, pauseMenuPrefab, pause, infoTextPrefab, this);
		currentTrack.StartRace();
	}
}