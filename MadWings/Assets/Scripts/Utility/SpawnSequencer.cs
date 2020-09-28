using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSequencer : MonoBehaviour
{
	[Serializable]
	public struct SpawnInfo
	{
		public GameObject[] objectTypes;
		public float delay;
	}

	public SpawnInfo[] spawnInfo;
}
