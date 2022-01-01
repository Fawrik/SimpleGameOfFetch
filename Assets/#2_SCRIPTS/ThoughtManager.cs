using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThoughtManager : MonoBehaviour
{
	public static ThoughtManager Instance;
	private void Awake()
	{
		if (Instance == null)
			Instance = this;
	}

	public ThoughtTrain currentThoughtTrain;
	public Thought currentThought;
	public ThoughtLine currentThoughtLine;
	public TextMeshProUGUI currentThoughtText;

	public bool inThought = false;

	public Animator thoughtAnim;

	public Queue<ThoughtLine> thoughtLines = new Queue<ThoughtLine>();
	public Queue<Thought> thoughts = new Queue<Thought>();

	float textLineWaitTime;

	public float newThoughtExpVal = 14;
	float newThoughtCapVal = 24;
	Vector2 newThoughtIncrementMinMax = new Vector2(2, 4);

	public float continueThoughtExpVal = 0;
	float continueThoughtCap = 5;

	public int thoughTrainIndex = 0;

	public int pickUpChance;

	public int earlyBallStop = 0;

	public bool inThoughtLines = false;

	public int forcePickUpCount;

	void Start()
    {
		//currentThoughtTrain = null;
    }
    
    void Update()
    {
		//ManualThoughtTrigger();
    }

	void UpdateThoughtTrain()
	{
		if (currentThoughtTrain != null)
		{
			return;
		}
		else
		{

		}
	}

	void ManualThoughtTrigger()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			StartCoroutine(EnqueueThoughts(currentThoughtTrain));
		}
		else if (Input.GetKeyDown(KeyCode.O))
		{
			//StartCoroutine(EnqueueThoughtLines(currentThought));
			DequeueThought();
		}
	}

	public void TriggerThought()
	{
		newThoughtExpVal += Random.Range(newThoughtIncrementMinMax.x, newThoughtIncrementMinMax.y);

		if (newThoughtExpVal >= newThoughtCapVal)
		{
			if (thoughTrainIndex < ThoughtTrainCollection.Instance.collection.Count)
			{
				currentThoughtTrain = ThoughtTrainCollection.Instance.collection[thoughTrainIndex];
				thoughTrainIndex++;
				newThoughtExpVal = 0;
				StartCoroutine(EnqueueThoughts(currentThoughtTrain));
			}
			
		}

		//currentThoughtText = currentThought.
	}

	public void TriggerObjectThought()
	{
		if (currentThoughtTrain == null)
		{
			//trigger object thought
		}
	}


	public IEnumerator EnqueueThoughts(ThoughtTrain tt)
	{

		
		inThought = true;
		thoughts.Clear();
		
		foreach (Thought thought in tt.thoughts)
		{
			thoughts.Enqueue(thought);
		}

		yield return new WaitForSeconds(2f);
		Man.Instance.audS.PlayOneShot(Man.Instance.sfx[Random.Range(0, Man.Instance.sfx.Count)]);

		currentThought = thoughts.Dequeue();
		StartCoroutine(EnqueueThoughtLines(currentThought));
	}

	public IEnumerator EnqueueThoughtLines(Thought t)
	{
		thoughtLines.Clear();
		foreach ( ThoughtLine thoughtLine in t.thoughtLines)
		{
			thoughtLines.Enqueue(thoughtLine);
		}

		yield return null;

		StartCoroutine(DequeueThoughtLines(currentThought));
		
	
	}

	public IEnumerator DequeueThoughtLines(Thought t)
	{
		inThoughtLines = true;
		for (int i = 0; i < t.thoughtLines.Count; i++)
		{
			currentThoughtLine = thoughtLines.Dequeue();
			//yield return new WaitForSeconds(4.5f);
			currentThoughtText.text = currentThoughtLine.thoughtLineText;

			thoughtAnim.SetBool("isShown", true);
			yield return new WaitForSeconds(1.5f);
			yield return new WaitForSeconds(4.5f);
			thoughtAnim.SetBool("isShown", false);
			yield return new WaitForSeconds(2f);
		}
		inThought = false;
		inThoughtLines = false;
		earlyBallStop++;
		if (thoughts.Count == 0)
		{
			currentThoughtTrain = null;
		}
	}

	public void DequeueThought()
	{

		//Counter 
		if (inThought == false && inThoughtLines == false)
		{
			continueThoughtExpVal += 1;
			continueThoughtCap = Random.Range(4, 9);
			earlyBallStop++;

			if (continueThoughtExpVal >= continueThoughtCap)
			{
				if (thoughts.Count > 0)
				{
					currentThought = thoughts.Dequeue();
					StartCoroutine(EnqueueThoughtLines(currentThought));

				
				}
				else if (thoughts.Count <= 0)
				{
					currentThoughtTrain = null;
				}

				continueThoughtExpVal = 0;
			}


		}

		
	}

}
