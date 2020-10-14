using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[Header("Resources")]
	public GameObject[] objects;
	public GameObject[] spawnPoints;
	public PoolType poolType;

	[Header("Settings")]
	public bool isSpawnAllObject;
	[Range(0, 50)] public int objectType;
	[Range(1, 50)] public int spawnType = 1;

	public float fireRate = 1f;
	private float nextFire = 0f;


	private void Update()
	{
		if (Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Spawn();
		}
	}

	public virtual void Spawn()
	{
		SpawnObject(poolType, 0);
	}

	public void SpawnObject(PoolType type, float rotateAngle)
	{
		//var bullet = ManagerPool.Instance.Spawn(type, objects[objectType]);
		//bullet.transform.position = spawnPoint.transform.position;
		//bullet.transform.rotation = Quaternion.Euler(0, 0, rotateAngle);

		foreach (var spawnPoint in spawnPoints)
		{
			var bullet = ManagerPool.Instance.Spawn(type, objects[objectType]);
			bullet.transform.position = spawnPoint.transform.position;
			bullet.transform.rotation = Quaternion.Euler(0, 0, rotateAngle);
		}
	}
}