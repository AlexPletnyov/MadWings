using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Enemy : MonoBehaviour, ICharacter, IPoolable
{
	[SerializeField] private float hp;
	private float startHp;
	private Vector3 startPosition;
	public int damage;
	public bool destroyBehindBounds;
	private Vector2 destroyAllCollSize;
	private Controller2D controller;

	private void Awake()
	{
		controller = GetComponent<Controller2D>();
		startHp = hp;
		startPosition = transform.position;
	}

	private void Start()
	{
		destroyAllCollSize = DestroyArea.colllider.size / 2;
	}

	private void Update()
	{
		if (destroyBehindBounds)
		{
			if (transform.position.x > destroyAllCollSize.x ||
			    transform.position.x < -destroyAllCollSize.x ||
			    transform.position.y > destroyAllCollSize.y ||
			    transform.position.y < -destroyAllCollSize.y)
			{
				Destruction();
			}

			destroyBehindBounds = false;
		}
	}

	public void GetDamage(int damage)
	{
		startHp -= damage;

		if (startHp <= 0)
		{
			Destruction();
		}
	}

	public void Destruction()
	{
		ManagerPool.Instance.Despawn(PoolType.Enemys, gameObject);
	}

	public void OnSpawn()
	{
	}

	public void OnDespawn()
	{
		startHp = hp;
		transform.position = startPosition;
		var pathFollower = GetComponent<PathFollower>();
		pathFollower.distanceTraveled = 0;
	}
}