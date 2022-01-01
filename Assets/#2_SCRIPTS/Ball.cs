using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public static Ball Instance;
	private void Awake()
	{
		if (Instance == null)
			Instance = this;
	}

	public List<AudioClip> sfx = new List<AudioClip>();

	public AudioSource audS;

	public Transform ballOutOfScreen;
	public Transform ballReadyForThrow;
	public BallState ballState = BallState.ReadyThrow;

	public Sprite currentSprite;

	public int ballIndex = 0;

	public PickUp currentBall;
	public PickUp tennisBall;


	void Start()
    {
        
    }

    
    void Update()
    {
		BallTransform();
    }

	public void BallChange()
	{
		if (ballIndex < BallCollection.Instance.collection.Count)
		{
			currentBall = BallCollection.Instance.collection[ballIndex];
			ballIndex++;

			gameObject.GetComponent<SpriteRenderer>().sprite = currentBall.sprite;

			if (currentBall.thought != null)
			{
				ThoughtManager.Instance.currentThoughtTrain = currentBall.thought;

				StartCoroutine(ThoughtManager.Instance.EnqueueThoughts(ThoughtManager.Instance.currentThoughtTrain));
			}
			
		}
		else
		{
			currentBall = tennisBall;
			gameObject.GetComponent<SpriteRenderer>().sprite = currentBall.sprite;
		}
	}

	public void BallTransform()
	{
		if (ballState == BallState.Throwing)
		{
			transform.position = Man.Instance.throwArch.position;
		}
		else if (ballState == BallState.ReadyFetch)
		{
			transform.position = ballOutOfScreen.position;
		}
		else if (ballState == BallState.Fetching)
		{
			transform.position = Dog.Instance.fetchCarry.position;
		}
		else if(ballState == BallState.ReadyThrow)
		{

			//transform.position = ballReadyForThrow.position;
		}
	}

	public IEnumerator BallSFX()
	{
		yield return new WaitForSeconds(1.2f);

		audS.PlayOneShot(sfx[Random.Range(0, sfx.Count)]);
	}

	public enum BallState { ReadyThrow, Throwing, ReadyFetch, Fetching, Idle}
}
