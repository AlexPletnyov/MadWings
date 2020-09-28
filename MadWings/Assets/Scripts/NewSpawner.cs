using System;
using System.Collections;
using UnityEngine;

public class NewSpawner : MonoBehaviour
{
	[Header("Resources")]
	public GameObject[] objects;
	public GameObject[] spawnPoints;
	[HideInInspector] public ObjectPooler.ObjectInfo.ObjectType[] objectTypes;
	public PoolType poolType;

	[Header("Settings")]
	public bool isSpawnAllObject;
	[Range(0, 50)] public int objectType;
	[Range(1, 50)] public int spawnType = 1;
	[Tooltip("0 = infinity")] public int amount;
	public float delay = 0.1f;

	private bool isSpawn = true;
	private bool canSpawnAfterDelay = true;
	private int infinity = 0;

	private void Awake()
	{
		objectTypes = new ObjectPooler.ObjectInfo.ObjectType[objects.Length];
		for (int i = 0; i < objects.Length; i++)
		{
			objectTypes[i] = objects[i].GetComponent<IPooledObject>().Type;
		}

		for (int i = 0; i < spawnPoints.Length; i++)
		{
			if (spawnPoints[i] == null)
			{
				spawnPoints[i] = gameObject;
			}
		}
	}

	private void Update()
	{
		if (canSpawnAfterDelay)
		{
			isSpawn = true;
		}
	}

	protected void FixedUpdate()
	{
		if (isSpawn && objectType <= objectTypes.Length - 1)
		{
			if (amount == infinity)
			{
				Spawn();
				StartCoroutine(SpawnDelay(delay));
				isSpawn = false;
			}
			else if (amount > 0)
			{
				StartCoroutine(LimitedSpawn(amount, delay));
				isSpawn = false;
				canSpawnAfterDelay = false;
			}
		}
	}

	public virtual void Spawn()
	{
		for (int i = 0; i < spawnPoints.Length; i++)
		{
			SpawnObject(poolType, spawnPoints[i]);
		}
	}

	public void SpawnObject(PoolType type, GameObject spawnPoint)
	{
		var bullet = ManagerPool.Instance.Spawn(type, objects[objectType]);
		bullet.transform.position = spawnPoint.transform.position;
	}

	//public void SpawnObject(ObjectPooler.ObjectInfo.ObjectType type, GameObject spawnPoint, float rotateAngle)
	//{
	//	var bullet = ObjectPooler.Instance.GetObject(type);
	//	bullet.transform.position = spawnPoint.transform.position;
	//	bullet.transform.rotation = Quaternion.Euler(0, 0, rotateAngle);
	//}

	IEnumerator SpawnDelay(float delay)
	{
		canSpawnAfterDelay = false;
		yield return new WaitForSeconds(delay);
		canSpawnAfterDelay = true;
	}

	IEnumerator LimitedSpawn(int amount, float delay)
	{
		for (int i = 0; i < amount; i++)
		{
			Spawn();
			yield return new WaitForSeconds(delay);
		}
	}
}