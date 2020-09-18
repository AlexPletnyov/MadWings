using System;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Bullet : MonoBehaviour, IPooledObject
{
	public ObjectPooler.ObjectInfo.ObjectType Type => type;

	[SerializeField]
	private ObjectPooler.ObjectInfo.ObjectType type;

	[SerializeField] private float speed;

	[SerializeField] private int damage;

	private Controller2D controller;
	private bool isPlayerBullet;
	private bool isEnemyBullet;

	private void Awake()
	{
		controller = GetComponent<Controller2D>();
		controller.speed = speed;
		isPlayerBullet = gameObject.CompareTag("PlayerBullet");
		isEnemyBullet = gameObject.CompareTag("EnemyBullet");
	}

	private void Update()
	{
		controller.speed = speed;
	}

	private void FixedUpdate()
	{
		controller.MoveAxisY();
	}

	public void OnTriggerEnter2D(Collider2D coll)
	{
		//if (pool == null) return;

		switch (coll.tag)
		{
			case "Enemy":
				if (isPlayerBullet)
				{
					coll.GetComponent<ICharacter>().GetDamage(damage);
					ObjectPooler.Instance.DestroyObject(gameObject);
				}
				break;

			case "Player":
				if (isEnemyBullet)
				{
					coll.GetComponent<ICharacter>().GetDamage(damage);
					ObjectPooler.Instance.DestroyObject(gameObject);
				}
				break;
		}
	}
}