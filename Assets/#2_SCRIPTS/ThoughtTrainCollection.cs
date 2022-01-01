using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtTrainCollection : MonoBehaviour
{
	public static ThoughtTrainCollection Instance;
	private void Awake()
	{
		if (Instance == null)
			Instance = this;

		CollectThoughts();
		ShuffleThoughts();
	}

	public List<ThoughtTrain> collection = new List<ThoughtTrain>();

	string thoughtsPath = "Thoughts/";

	public void CollectThoughts()
	{
		collection.Clear();

		var dials = Resources.LoadAll<ThoughtTrain>(thoughtsPath);

		foreach (var item in dials)
		{
			collection.Add(item);
		}
	}

	public void ShuffleThoughts()
	{
		for (int i = 0; i < collection.Count; i++)
		{
			ThoughtTrain temp = collection[i];
			int randomIndex = Random.Range(i, collection.Count);
			collection[i] = collection[randomIndex];
			collection[randomIndex] = temp;
		}
	}


	public void Update()
	{
		//if (Input.GetKeyDown(KeyCode.L))
		//{
		//	CollectThoughts();
		//}
		//else if (Input.GetKeyDown(KeyCode.K))
		//{
		//	ShuffleThoughts();
		//}

	}
}
