using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour {
	[SerializeField] private TrackHandler[] trackPrefabs;
	private TrackHandler currentTrack;
	[SerializeField] private CarHandler[] carPrefabs;
	[SerializeField] private PowerUpTrigger[] powerUpPrefabs;
	[SerializeField] private MainMenu mainMenuPrefab;
	[SerializeField] private RaceEndMenu raceEndMenuPrefab;
	[SerializeField] private InfoText infoTextPrefab;

	private void Awake(){
		LoadMainMenu();
	}
	public void LoadMainMenu(){
		MainMenu mainMenu = Instantiate(mainMenuPrefab);
		for (int i = 0; i < trackPrefabs.Length && i < mainMenu.Buttons.Length; i++){
			TrackHandler trackPrefab = trackPrefabs[i];
			mainMenu.Buttons[i].onClick.AddListener(() => {
				LoadTrack(trackPrefab);
				Destroy(mainMenu.gameObject);
			});
		}
	}
	public void LoadTrack(TrackHandler trackPrefab){
		if (currentTrack != null){
			Destroy(currentTrack.gameObject);
		}
		currentTrack = Instantiate(trackPrefab);
		currentTrack.Initialize(carPrefabs, powerUpPrefabs, raceEndMenuPrefab, infoTextPrefab, this);
		currentTrack.StartRace();
	}
}