using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameUnitsSpawner
{
	public GameObject @object;

	[Range(0f, 300f)] public float delay;
	public int group;
}

public class SpawnSequencer : MonoBehaviour
{
	private enum StartType { OnEnable, Awake }
	private enum SequencerType { Time, Step }

	public int group;

	[SerializeField] private SequencerType sequencerType = SequencerType.Time;
	[SerializeField] private StartType startType = StartType.OnEnable;

	public GameUnitsSpawner[] elements;

	[HideInInspector] public bool endSequence;

	private bool usingPool;

	private void Awake()
	{
		if (startType == StartType.Awake)
		{
			StartSequence();
		}
	}

	private void OnEnable()
	{
		if (startType == StartType.OnEnable)
		{
			StartSequence();
		}
	}

	private void StartSequence()
	{
		switch (sequencerType)
		{
			case SequencerType.Time:
				StartTimeSequencer();
				break;

			case SequencerType.Step:
				StartStepSequencer();
				break;
		}
	}

	private void StartTimeSequencer()
	{
		for (int i = 0; i < elements.Length; i++)
		{
			usingPool = elements[i].@object.GetComponent<Pool>() != null;

			StartCoroutine(SpawnByTime(elements[i].delay, elements[i].@object, usingPool));

			if (i == elements.Length - 1)
			{
				endSequence = true;
			}
		}
	}

	private void StartStepSequencer()
	{
		//for (int i = 0; i < elements.Length; i++)
		//{
		//	if (!elements[i].@object.GetComponent<SpawnSequencer>().endSequence)
		//	{
		//		for (int j = 0; j < elements[i].@object.GetComponent<SpawnSequencer>().elements.Length; j++)
		//		{
		//			if (elements[i].@object.GetComponent<SpawnSequencer>().endSequence && !elements[i].@object.activeSelf)
		//			{

		//			}
		//		}
		//	}
		//}


	}

	IEnumerator SpawnByTime(float delay, GameObject element, bool usingPool)
	{
		if (delay != 0)
		{
			yield return new WaitForSeconds(delay);
		}

		if (Player.instance != null)
		{
			if (usingPool)
			{
				element.GetComponent<Spawner>().start = true;
			}
			else
			{
				element.SetActive(true);
			}
		}
	}
}
