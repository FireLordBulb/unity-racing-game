using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.CrashReportHandler;

public class TrackHandler : MonoBehaviour {
	[SerializeField] private FinishLineTrigger finishLine;
	private CarHandler[] cars;
	public void SetUp(CarHandler[] carPrefabs, Transform carParent){
		Vector2 carPos = ((Vector2)finishLine.transform.position) + finishLine.leftMostCarPosition;
		Quaternion rotation = finishLine.transform.rotation;
		Vector2 carPosDifference = (finishLine.rightMostCarPosition-finishLine.leftMostCarPosition)/(cars.Length-1);
		cars = new CarHandler[carPrefabs.Length];
		for (int i = 0; i < carPrefabs.Length; i++){
			cars[i] = Instantiate(carPrefabs[i], carPos, rotation, carParent);
			carPos += carPosDifference;
		}
	}
	public void StartRace(){
		foreach (var car in cars){
			car.EnableUpdate();
		}
	}
}