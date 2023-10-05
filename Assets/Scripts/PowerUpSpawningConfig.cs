using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class PowerUpSpawningConfig : ScriptableObject {
	[SerializeField]private int maxInstances;
	[SerializeField]private float maxPositionOffset, minSpawnTime, maxSpawnTime;
	public int MaxInstances => maxInstances;
	public float NewPositionOffset => Random.Range(-maxPositionOffset, maxPositionOffset);
	public float NewSpawnTime => Random.Range(minSpawnTime, maxSpawnTime);
}