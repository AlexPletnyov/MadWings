using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BonusType
{
	IncreaseAttack,
	Health,
	KillAll,
}

[RequireComponent(typeof(Controller2D))]
public class Bonus : MonoBehaviour
{
	public BonusType bonusType;

	[SerializeField] private float speed;

	private Controller2D controller;

	private void Awake()
	{
		controller = GetComponent<Controller2D>();
		controller.speed = speed;
	}

	private void Update()
	{
		controller.speed = speed;
	}

	private void FixedUpdate()
	{
		MoveBonus();
	}

	private void MoveBonus()
	{
		controller.MoveAxisY();
	}

	public void DespawnBonus()
	{
		gameObject.SetActive(false);
	}
}