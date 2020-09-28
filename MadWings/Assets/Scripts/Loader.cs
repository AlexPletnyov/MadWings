using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
	public GameObject prefab;
	public GameObject bullet;


	public List<GameObject> obj = new List<GameObject>();

	private void Awake()
	{
		ManagerPool.Instance.AddPool(PoolType.Entities).PopulateWith(prefab, 50);
		ManagerPool.Instance.AddPool(PoolType.Bullets).PopulateWith(bullet, 50);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			obj.Add(ManagerPool.Instance.Spawn(PoolType.Entities, prefab));
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			for (int i = 0; i < obj.Count; i++)
			{
				ManagerPool.Instance.Despawn(PoolType.Entities, obj[i]);
			}
			obj.Clear();
		}
	}
}
