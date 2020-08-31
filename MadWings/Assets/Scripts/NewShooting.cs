using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewShooting : MonoBehaviour
{
	public ObjectPooler.ObjectInfo.ObjectType bulletType;

	[SerializeField]
	private GameObject firePoint;

	private void Update() 
	{
		var bullet = ObjectPooler.Instance.GetObject(bulletType);
		bullet.transform.position = firePoint.transform.position;
		bullet.transform.rotation = Quaternion.Euler(0, 0, 0);

	}


}
