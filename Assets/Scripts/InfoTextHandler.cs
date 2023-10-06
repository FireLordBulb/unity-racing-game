using TMPro;
using UnityEngine.UIElements;

public class InfoTextHandler {
	private const int SmallestTwoDigitInt = 10, SecondsInAMinute = 60;
	private readonly TextMeshProUGUI timer, lapCounter;
	private readonly int totalLaps;
	public InfoTextHandler(InfoText infoText, string trackName, int tl){
		infoText.TrackName.text = trackName;
		timer = infoText.Timer;
		lapCounter = infoText.LapCounter;
		totalLaps = tl;
	}
	public void UpdateTimer(float seconds){
		int hundreds = (int)(seconds%1*100);
		int wholeSeconds = (int)(seconds%SecondsInAMinute);
		int minutes = (int)(seconds/SecondsInAMinute);
		timer.text = $"{AddLeadingZeroIfSingleDigit(minutes)}:{AddLeadingZeroIfSingleDigit(wholeSeconds)}.{AddLeadingZeroIfSingleDigit(hundreds)}";
	}
	private static string AddLeadingZeroIfSingleDigit(int n){
		return n < SmallestTwoDigitInt ? $"0{n}" : $"{n}";
	}
	public void UpdateLapCounter(int lap){
		lapCounter.text = $"{lap}/{totalLaps}";
	}
}