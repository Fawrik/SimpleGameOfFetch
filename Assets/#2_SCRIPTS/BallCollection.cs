using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollection : MonoBehaviour
{
	public static BallCollection Instance;
	private void Awake()
	{
		if (Instance == null)
			Instance = this;

		CollectBalls();
		ShuffleBalls();
	}

	public List<PickUp> collection = new List<PickUp>();

	string ballsPath = "Balls/";

	public void CollectBalls()
	{
		collection.Clear();

		var dials = Resources.LoadAll<PickUp>(ballsPath);

		foreach (var item in dials)
		{
			collection.Add(item);
		}
	}

	public void ShuffleBalls()
	{
		for (int i = 0; i < collection.Count; i++)
		{
			PickUp temp = collection[i];
			int randomIndex = Random.Range(i, collection.Count);
			collection[i] = collection[randomIndex];
			collection[randomIndex] = temp;
		}
	}
}
