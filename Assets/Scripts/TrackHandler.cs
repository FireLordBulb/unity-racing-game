using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackHandler : MonoBehaviour {
	[SerializeField] private FinishLineTrigger finishLine;
	private CarHandler[] cars;
	public void SetUp(CarHandler[] carPrefabs, Transform carParent){
		Vector2 leftmostCarPosition = finishLine.leftmostCarTransform.position;
		Vector2 rightmostCarPosition = finishLine.rightmostCarTransform.position;
		Vector2 carPos = leftmostCarPosition;
		Quaternion rotation = finishLine.transform.rotation;
		// Divide the distance between the two extremes by the number of gaps between cars, so for example 3 cars would have 
		// 2 gaps between cars so the difference is half of the total distance.
		Vector2 carPosDifference = (rightmostCarPosition-leftmostCarPosition)/Mathf.Max(cars.Length-1, 1);
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