using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	[SerializeField] private Button[] levelButtons = new Button[3];
	public Button[] LevelButtons => levelButtons;
}