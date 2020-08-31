using System.Net.Mail;
using System.Dynamic;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [Serializable]
    public struct ObjectInfo
    {
        public enum ObjectType
        {
            Bullet_1,
            Bullet_2,
            Bullet_3,
            Bullet_4,
        }

        public ObjectType Type;
        public GameObject Prefab;
        public int StartCount;
    }

    [SerializeField]
    private List<ObjectInfo> objectInfo;
	private Dictionary<ObjectInfo.ObjectType, NewPool> pools;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            InitPool();
        }
    }

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
		var obj = pools[type].Objects.Count > 0 ? 
		pools[type].Objects.Dequeue() : InstantiateObject(type, pools[type].Container);
		obj.SetActive(true);
		return obj;
	}

	public void DestroyObject(GameObject obj)
	{
		pools[obj.GetComponent<IPooledObject>().Type].Objects.Enqueue(obj);
		obj.SetActive(false);
	}
}
