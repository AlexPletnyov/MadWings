using System.ComponentModel;
using System.Collections.Generic;
using UnityEngine;

public class NewPool : MonoBehaviour
{
	public Transform Container { get; private set; }

	public Queue<GameObject> Objects;

	public NewPool (Transform container)
	{
		Container = container;
		Objects = new Queue<GameObject>();
	}

	
}