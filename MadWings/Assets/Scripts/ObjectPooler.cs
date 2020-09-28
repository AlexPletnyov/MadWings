using System;
using UnityEngine;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
	[Serializable]
    public struct ObjectInfo
    {
        public enum ObjectType
        {
            Bullet_1,
            Bullet_2,
            Bullet_3,
            Bullet_4,

			Enemy_1,
			Enemy_2,
			Enemy_3,
			Enemy_4,
			Enemy_5,
			Enemy_6,
			Enemy_7,
			Enemy_8,
			Enemy_9,
			Enemy_10
		}

        public ObjectType Type;
        public GameObject Prefab;
        public int StartCount;
    }

    [SerializeField]
    private List<ObjectInfo> objectInfo;
	private Dictionary<ObjectInfo.ObjectType, NewPool> pools;

	#region Singleton

	public static ObjectPooler Instance;
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}

		InitPool();
	}

	#endregion

	private void InitPool()
    {
		pools = new Dictionary<ObjectInfo.ObjectType, NewPool>();

		var emptyGO = new GameObject();

		foreach (var obj in objectInfo)
		{
			var container = Instantiate(emptyGO, transform, false);

			container.name = obj.Type.ToString();

			pools[obj.Type] = new NewPool(container.transform);

			for (int i = 0; i < obj.StartCount; i++)
			{
				var go = InstantiateObject(obj.Type, container.transform);
				pools[obj.Type].Objects.Enqueue(go);
			}
		}
		Destroy(emptyGO);
    }

	private GameObject InstantiateObject(ObjectInfo.ObjectType type, Transform parent)
	{
		var go = Instantiate(objectInfo.Find(x => x.Type == type).Prefab, parent);

		go.SetActive(false);

		return go;
	}

	public GameObject GetObject(ObjectInfo.ObjectType type)
	{
		var obj = pools[type].Objects.Count != 0 ? pools[type].Objects.Dequeue() : InstantiateObject(type, pools[type].Container);
		obj.SetActive(true);
		return obj;
	}

	public void DestroyObject(GameObject obj)
	{
		obj.SetActive(false);
		pools[obj.GetComponent<IPooledObject>().Type].Objects.Enqueue(obj);
	}
}
