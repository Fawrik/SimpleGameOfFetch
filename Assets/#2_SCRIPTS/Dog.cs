using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
	public static Dog Instance;
	private void Awake()
	{
		if (Instance == null)
			Instance = this;
	}

	public List<AudioClip> sfx = new List<AudioClip>();
	public AudioSource audS;

	public Animator dogAnim;
	public AnimationClip fetching;
	public AnimationClip fetched;

	public Transform fetchCarry;

	float dogFetchReactionDelay;

	Vector2 dogFetchReactionMinMax = new Vector2(1.4f, 2f);

	void Start()
    {
		dogAnim = GetComponent<Animator>();
    }

    
    void Update()
    {
        
    }

	void PlayDogBark()
	{
		int ii = 1;

		if (ii == Random.Range(1, 4))
		{
			audS.PlayOneShot(sfx[Random.Range(0, sfx.Count)]);
		}

	}


	public IEnumerator Fetch()
	{
		dogFetchReactionDelay = Random.Range(dogFetchReactionMinMax.x, dogFetchReactionMinMax.y);
		yield return new WaitForSeconds(Man.Instance.throwAnimClip.length / dogFetchReactionDelay);
		dogAnim.SetInteger("Fetch", 1);
		PlayDogBark();

		yield return new WaitForSeconds(fetching.length);


		yield return new WaitUntil(() => Ball.Instance.ballState == Ball.BallState.ReadyFetch);


		if (ThoughtManager.Instance.currentThoughtTrain == null && ThoughtManager.Instance.newThoughtExpVal > 6)
		{
			int i = 1;
			ThoughtManager.Instance.earlyBallStop++;

			ThoughtManager.Instance.pickUpChance = Random.Range(1, 4);

			if (i == ThoughtManager.Instance.pickUpChance && ThoughtManager.Instance.earlyBallStop > 4 || ThoughtManager.Instance.forcePickUpCount > 12)
			{
				ThoughtManager.Instance.forcePickUpCount = 0;

				if (ThoughtManager.Instance.newThoughtExpVal > 19)
				{
					ThoughtManager.Instance.newThoughtExpVal = 9;
				}

				Ball.Instance.BallChange();

			}
		}

		Ball.Instance.ballState = Ball.BallState.Fetching;
		PlayDogBark();
		yield return new WaitForSeconds(1.2f); //How long until dog return with ball


		dogAnim.SetInteger("Fetch", 2);

		if (ThoughtManager.Instance.currentThoughtTrain == null) // && Dog is not carrying new ball
		{

			ThoughtManager.Instance.TriggerThought();
		}
		else
		{
			ThoughtManager.Instance.DequeueThought();
		}

		yield return new WaitForSeconds(fetched.length);
		dogAnim.SetInteger("Fetch", 0);

		yield return new WaitForSeconds(0.4f);
		Ball.Instance.ballState = Ball.BallState.ReadyThrow;

		

		Ball.Instance.transform.position = Ball.Instance.ballReadyForThrow.position;

	}
}
