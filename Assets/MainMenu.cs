using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	[SerializeField] private Button[] buttons = new Button[3];
	public Button[] Buttons => buttons;
}