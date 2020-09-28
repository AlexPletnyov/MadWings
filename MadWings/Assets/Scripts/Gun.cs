using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Spawner
{
	public override void Spawn()
	{
		switch (spawnType)
		{
			case 1:
				SpawnObject(objectTypes[objectType], spawnPoints[0], 0);
				break;
			case 2:
				SpawnObject(objectTypes[objectType], spawnPoints[1], 0);
				SpawnObject(objectTypes[objectType], spawnPoints[2], 0);
				break;
			case 3:
				SpawnObject(objectTypes[objectType], spawnPoints[0], 0);
				SpawnObject(objectTypes[objectType], spawnPoints[1], 5);
				SpawnObject(objectTypes[objectType], spawnPoints[2], -5);
				break;
		}
	}
}
