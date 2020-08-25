using System;
using UnityEditor;
using UnityEngine;

public class DestroyArea : MonoBehaviour
{
	public static BoxCollider2D colllider;
	private Vector2 viewportSize;
	private Bounds colBounds;

	private void Awake()
	{
		colllider = GetComponent<BoxCollider2D>();
	}

	private void Start()
	{
		ResizeCollider();
	}

	void ResizeCollider()
	{
		viewportSize = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) * 2;

		viewportSize.x *= 2f;
		viewportSize.y *= 1.5f;

		colllider.size = viewportSize;
		colBounds = colllider.bounds;
	}

	public void OnTriggerExit2D(Collider2D coll)
	{
		switch (coll.tag)
		{
			case "PlayerBullet":
				coll.GetComponentInParent<Pool>().Despawn(coll.gameObject);
				break;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		Gizmos.DrawLine(new Vector2(colBounds.min.x, colBounds.min.y), new Vector2(colBounds.max.x, colBounds.min.y));
		Gizmos.DrawLine(new Vector2(colBounds.min.x, colBounds.min.y), new Vector2(colBounds.min.x, colBounds.max.y));
		Gizmos.DrawLine(new Vector2(colBounds.min.x, colBounds.max.y), new Vector2(colBounds.max.x, colBounds.max.y));
		Gizmos.DrawLine(new Vector2(colBounds.max.x, colBounds.max.y), new Vector2(colBounds.max.x, colBounds.min.y));
	}
}