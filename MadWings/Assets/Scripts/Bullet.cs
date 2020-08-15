using System;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Bullet : MonoBehaviour
{
	[SerializeField] private float speed;

	[SerializeField] private int damage;

	private Controller2D controller;
	private bool isPlayerBullet;
	private bool isEnemyBullet;
	private Pool pool;

	private void Awake()
	{
		controller = GetComponent<Controller2D>();
		pool = GetComponentInParent<Pool>();
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
					GetComponentInParent<Pool>().Despawn(gameObject);
				}
				break;

			case "Player":
				if (isEnemyBullet)
				{
					coll.GetComponent<ICharacter>().GetDamage(damage);
					GetComponentInParent<Pool>().Despawn(gameObject);
				}
				break;
		}
	}
}