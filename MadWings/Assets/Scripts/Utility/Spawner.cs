using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private ObjectPooler.ObjectInfo.ObjectType[] pools;
	[SerializeField, Range(0, 20)] public int type = 0;
	[SerializeField] private GameObject[] spawnPoints;
	private bool isSpawn = true;
	private bool canSpawnAfterDelay = true;
	private float shootingDelay;


	private void Update()
	{
		if (canSpawnAfterDelay)
		{
			isSpawn = true;
		}
	}

	protected void FixedUpdate()
	{
		if (isSpawn)
		{
			Spawn();
			StartCoroutine(ShootingDelay(shootingDelay));
			isSpawn = false;
		}
	}

	public virtual void Spawn()
	{
		ObjectPooler.Instance.SpawnObject(pools[type], spawnPoints[0], 0);
	}

	IEnumerator ShootingDelay(float delay)
	{
		canSpawnAfterDelay = false;
		yield return new WaitForSeconds(delay);
		canSpawnAfterDelay = true;
	}
}