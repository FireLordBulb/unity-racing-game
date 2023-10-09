using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaceEndMenu : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI result;
	[SerializeField] private Button playAgain, returnToMenu;
	public TextMeshProUGUI Result => result;
	public Button PlayAgain => playAgain;
	public Button ReturnToMenu => returnToMenu;
}