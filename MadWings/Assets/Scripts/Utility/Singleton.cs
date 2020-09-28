using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowWhale
{
	public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
	{
		public static bool isApplicationQuitting;

		private static T _instance;

		public static T Instance
		{
			get
			{
				if (isApplicationQuitting)
				{
					return null;
				}

				if (_instance == null)
				{
						_instance = FindObjectOfType<T>();

						if (_instance == null)
						{
							var singleton = new GameObject("[SINGLETON] " + typeof(T));
							_instance = singleton.AddComponent<T>();
							DontDestroyOnLoad(_instance);
						}
				}

				return _instance;
			}
		}

		public virtual void OnDestroy()
		{
			isApplicationQuitting = true;
		}
	}
}
