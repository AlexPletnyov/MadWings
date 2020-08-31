using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBullet : MonoBehaviour, IPooledObject
{
	public ObjectPooler.ObjectInfo.ObjectType Type => type;

	[SerializeField]
	private ObjectPooler.ObjectInfo.ObjectType type;
}
