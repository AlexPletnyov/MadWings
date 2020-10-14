using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Pool : MonoBehaviour
{
	#region OldVersion

	//public GameObject[] objects;
	//public int startingPoolSize = 5;

	//[Tooltip("Если неактивно, то в пуле будет только первый объект массива")]
	//[SerializeField] private bool randomCreate;

	//private Queue<GameObject> currentObjects;

	//private void Awake()
	//{
	//	currentObjects = new Queue<GameObject>();
	//}

	//private void Start()
	//{
	//	for (int i = 0; i < startingPoolSize; i++)
	//	{
	//		CreateObject(false, true);
	//	}
	//}

	//public GameObject Spawn()
	//{
	//	if (currentObjects.Count != 0)
	//	{
	//		var obj = currentObjects.Dequeue();

	//		obj.SetActive(true);

	//		return obj;
	//	}
	//	else
	//	{
	//		return CreateObject(true, false); ;
	//	}
	//}

	//public void Despawn(GameObject _object)
	//{
	//	if (_object != null)
	//	{
	//		_object.SetActive(false);

	//		currentObjects.Enqueue(_object);
	//	}
	//}

	//private GameObject CreateObject(bool setActive, bool addToQueue)
	//{
	//	GameObject obj = null;

	//	if (objects != null)
	//	{
	//		int index;

	//		if (randomCreate)
	//		{
	//			index = Random.Range(0, objects.Length);
	//		}
	//		else
	//		{
	//			index = 0;
	//		}

	//		obj = Instantiate(objects[index]);
	//		obj.SetActive(setActive);
	//		obj.transform.parentPool = transform;

	//		if (addToQueue)
	//		{
	//			currentObjects.Enqueue(obj);
	//		}

	//		return obj;
	//	}
	//	else
	//	{
	//		print("Добавь префаб в pool.");
	//	}

	//	return obj;
	//}

	//void OnDestroy()
	//{
	//	objects = null;
	//	currentObjects = null;
	//}

	#endregion

	private Transform parentPool;
	private Dictionary<int, Stack<GameObject>> cachedObjects = new Dictionary<int, Stack<GameObject>>();
	private Dictionary<int, int> cachedIds = new Dictionary<int, int>();

	public Pool PopulateWith(GameObject prefab, int amount)
	{
		var key = prefab.GetInstanceID();
		var stack = new Stack<GameObject>(amount);
		cachedObjects.Add(key, stack);

		for (int i = 0; i < amount; i++)
		{
			var go = Populate(prefab, Vector3.zero, Quaternion.identity, parentPool);
			go.SetActive(false);
			cachedIds.Add(go.GetInstanceID(), key);
			cachedObjects[key].Push(go);
		}

		return null;
	}


	public void SetParent(Transform parent)
	{
		parentPool = parent;
	}

	public GameObject Spawn(GameObject prefab, Vector3 position = default(Vector3),
		Quaternion rotation = default(Quaternion), Transform parent = null)
	{
		var key = prefab.GetInstanceID();
		Stack<GameObject> stack;
		var stacked = cachedObjects.TryGetValue(key, out stack);

		if (stacked && stack.Count > 0)
		{
			var transform = stack.Pop().transform;
			transform.SetParent(parent);
			transform.rotation = rotation;
			transform.gameObject.SetActive(true);

			if (parent) transform.position = position;
			else transform.localPosition = position;

			var poolable = transform.GetComponent<IPoolable>();
			poolable?.OnSpawn();

			return transform.gameObject;
		}

		if (!stacked) cachedObjects.Add(key, new Stack<GameObject>());

		var createdPrefab = Populate(prefab, position, rotation, parent);
		cachedIds.Add(createdPrefab.GetInstanceID(), key);

		return createdPrefab;
	}

	public void Despawn(GameObject go)
	{
		if (go != null && !cachedObjects[cachedIds[go.GetInstanceID()]].Contains(go) && go.activeInHierarchy)
		{
			go.SetActive(false);

			cachedObjects[cachedIds[go.GetInstanceID()]].Push(go);

			var poolable = go.GetComponent<IPoolable>();
			poolable?.OnDespawn();

			if (parentPool != null)
			{
				go.transform.SetParent(parentPool);
			}
		}
	}

	public void Dispose()
	{
		parentPool = null;
		cachedObjects.Clear();
		cachedIds.Clear();
	}

	public GameObject Populate(GameObject prefab, Vector3 position = default(Vector3),
		Quaternion rotation = default(Quaternion), Transform parent = null)
	{
		var go = Object.Instantiate(prefab, position, rotation, parent).transform;

		if (parent == null)
		{
			go.position = position;
		}
		else
		{
			go.localPosition = position;
		}

		return go.gameObject;
	}
}