using System;
using System.Collections;
using System.Transactions;
using UnityEngine;

[RequireComponent(typeof(Pool))]
public class Spawner : MonoBehaviour
{
	public int amount;
	public float delay;
	public bool start = false;
	public bool despawnAll;

	private Pool pool;
	private GameObject[] objects;

	private void Awake()
	{
		pool = GetComponent<Pool>();
		pool.startingPoolSize = amount;

		if (amount == 0 && pool != null)
		{
			amount = pool.startingPoolSize;
		}

		objects = new GameObject[amount];
	}

	private void Despawn(GameObject obj)
	{
		pool.Despawn(obj);
	}

	private void Update()
	{
		if (start)
		{
			StartCoroutine(Spawn());
			start = false;
		}

		if (despawnAll)
		{
			for (int i = 0; i < objects.Length; i++)
			{
				Despawn(objects[i]);
			}

			objects = new GameObject[amount];

			despawnAll = false;
		}
	}

	IEnumerator Spawn()
	{
		for (int i = 0; i < amount; i++)
		{
			objects[i] = pool.Spawn();
			yield return new WaitForSeconds(delay);
		}
	}
}