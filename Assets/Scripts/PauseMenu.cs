using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	[SerializeField] private Button resumeGame, restartRace, returnToMenu;
	public Button ResumeGame => resumeGame;
	public Button RestartRace => restartRace;
	public Button ReturnToMenu => returnToMenu;
}