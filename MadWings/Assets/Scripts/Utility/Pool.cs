using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
	public GameObject[] objects;
	public int startingPoolSize = 5;

	[Tooltip("Если неактивно, то в пуле будет только первый объект массива")]
	[SerializeField] private bool randomCreate;

	private Queue<GameObject> currentObjects;

	private void Awake()
	{
		currentObjects = new Queue<GameObject>();
	}

	private void Start()
	{
		for (int i = 0; i < startingPoolSize; i++)
		{
			CreateObject(false, true);
		}
	}

	public GameObject Spawn()
	{
		if (currentObjects.Count != 0)
		{
			var obj = currentObjects.Dequeue();

			obj.SetActive(true);

			return obj;
		}
		else
		{
			return CreateObject(true, false); ;
		}
	}

	public void Despawn(GameObject _object)
	{
		if (_object != null)
		{
			_object.SetActive(false);

			currentObjects.Enqueue(_object);
		}
	}

	private GameObject CreateObject(bool setActive, bool addToQueue)
	{
		GameObject obj = null;

		if (objects != null)
		{
			int index;

			if (randomCreate)
			{
				index = Random.Range(0, objects.Length);
			}
			else
			{
				index = 0;
			}

			obj = Instantiate(objects[index]);
			obj.SetActive(setActive);
			obj.transform.parent = transform;

			if (addToQueue)
			{
				currentObjects.Enqueue(obj);
			}

			return obj;
		}
		else
		{
			print("Добавь префаб в pool.");
		}

		return obj;
	}

	void OnDestroy()
	{
		objects = null;
		currentObjects = null;
	}
}