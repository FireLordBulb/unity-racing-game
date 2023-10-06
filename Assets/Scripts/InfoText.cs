using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoText : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI trackName, timer, lapCounter;
	public TextMeshProUGUI TrackName => trackName;
	public TextMeshProUGUI Timer => timer;
	public TextMeshProUGUI LapCounter => lapCounter;
}