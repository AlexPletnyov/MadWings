using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewShooting : MonoBehaviour
{
	public ObjectPooler.ObjectInfo.ObjectType[] bulletType;

	[SerializeField]
	private GameObject[] firePoint;

	private void Update() 
	{
		Shoot();
	}

	private void Shoot()
	{
		var bullet = ObjectPooler.Instance.GetObject(bulletType[0]);
		bullet.transform.position = firePoint[0].transform.position;
		bullet.transform.rotation = Quaternion.Euler(0, 0, 0);
	}
}
