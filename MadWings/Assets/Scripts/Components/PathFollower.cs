using System;
using PathCreation;
using UnityEngine;
	
public class PathFollower : MonoBehaviour
{
	public PathCreator pathCreator;
	public EndOfPathInstruction end;
	public float speed = 5;

	public float distanceTraveled;
	private Vector2 path;
	private Quaternion angle;
	private Controller2D controller;

	private void Awake()
	{
		controller = GetComponent<Controller2D>();
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void Move()
	{
		distanceTraveled += speed * Time.deltaTime;

		path = pathCreator.path.GetPointAtDistance(distanceTraveled, end);

		angle.z = pathCreator.path.GetRotationAtDistance(distanceTraveled, end).z;
		angle.w = pathCreator.path.GetRotationAtDistance(distanceTraveled, end).w;

		controller.MovePosition(path);
		controller.MoveRotation(angle);
	}
}
