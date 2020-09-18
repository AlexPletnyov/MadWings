using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	private enum GunType
	{
		PlayerGun,
		EnemyGun
	}

	[Header("Resources")]
	[SerializeField] private ObjectPooler.ObjectInfo.ObjectType[] bulletPools;
	[SerializeField] private GameObject[] firePoints;

	[Header("Settings")]
	[SerializeField] private GunType gunType = GunType.EnemyGun;
	[SerializeField, Range(0, 20)] public int bulletType = 0;
	[SerializeField, Range(1, 20)] public int fireType = 1;
	[SerializeField, Range(1, 5)] public int damageModifier = 1;
	[SerializeField, Range(0.1f, 0.5f)] private float shootingDelay;

	private bool shoot = true;
	private bool canShootAfterDelay = true;

	private void Update()
	{
		if (canShootAfterDelay)
		{
			shoot = true;
		}
	}

	protected void FixedUpdate()
	{
		if (shoot)
		{
			Shoot();
			StartCoroutine(ShootingDelay(shootingDelay));
			shoot = false;
		}
	}

	private void Shoot()
	{
		switch (gunType)
		{
			case GunType.PlayerGun:
				PlayerShoot();
				break;

			case GunType.EnemyGun:
				EnemyShoot();
				break;
		}
	}

	private void PlayerShoot()
	{
		switch (fireType)
		{
			case 1:
				ObjectPooler.Instance.SpawnObject(bulletPools[bulletType], firePoints[0], 0);
				break;
			case 2:
				ObjectPooler.Instance.SpawnObject(bulletPools[bulletType], firePoints[1], 0);
				ObjectPooler.Instance.SpawnObject(bulletPools[bulletType], firePoints[2], 0);
				break;
			case 3:
				ObjectPooler.Instance.SpawnObject(bulletPools[bulletType], firePoints[0], 0);
				ObjectPooler.Instance.SpawnObject(bulletPools[bulletType], firePoints[1], 5);
				ObjectPooler.Instance.SpawnObject(bulletPools[bulletType], firePoints[2], -5);
				break;
		}
	}

	private void EnemyShoot()
	{
		switch (fireType)
		{
			case 1:

				break;
			case 2:

				break;
			case 3:

				break;
		}
	}

	IEnumerator ShootingDelay(float delay)
	{
		canShootAfterDelay = false;
		yield return new WaitForSeconds(delay);
		canShootAfterDelay = true;
	}
}
