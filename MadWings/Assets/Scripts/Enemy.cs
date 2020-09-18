using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Enemy : MonoBehaviour, ICharacter, IPooledObject
{
	public ObjectPooler.ObjectInfo.ObjectType Type => type;
	[SerializeField] private ObjectPooler.ObjectInfo.ObjectType type;
	[SerializeField] private float hp;
	public int damage;
	public bool destroyBehindBounds;
	private Vector2 destroyAllCollSize;
	private Controller2D controller;

	private void Awake()
	{
		controller = GetComponent<Controller2D>();
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
		hp -= damage;

		if (hp <= 0)
		{
			Destruction();
		}
	}

	public void Destruction()
	{
		ObjectPooler.Instance.DestroyObject(gameObject);
	}
}