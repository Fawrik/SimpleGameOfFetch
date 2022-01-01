using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : MonoBehaviour
{
	public static Man Instance;
	private void Awake()
	{
		if (Instance == null)
			Instance = this;
	}

	public bool canThrow = true;
	public List<AudioClip> sfx = new List<AudioClip>();
	public AudioSource audS;

	public Animator manAnim;
	public GameObject fetchObject;
	public Transform throwArch;
	public AnimationClip throwAnimClip;
	

    void Start()
    {
		manAnim = GetComponent<Animator>();
    }

    
    void Update()
    {
		ThrowBall();
    }

	public void ThrowBall()
	{
		if (Ball.Instance.ballState == Ball.BallState.ReadyThrow && canThrow)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Ball.Instance.ballState = Ball.BallState.Throwing;

				StartCoroutine(ThrowingBall());
			}
		}
	}



	public IEnumerator ThrowingBall()
	{
		ThoughtManager.Instance.forcePickUpCount++;
		manAnim.SetTrigger("Throw");
		StartCoroutine(Ball.Instance.BallSFX());
		StartCoroutine(Dog.Instance.Fetch());
		yield return new WaitForSeconds(throwAnimClip.length);
		Ball.Instance.ballState = Ball.BallState.ReadyFetch;

		if (ThoughtManager.Instance.currentThoughtTrain == null) // && Dog is not carrying new ball
		{

			ThoughtManager.Instance.TriggerThought();
		}
		else
		{
			ThoughtManager.Instance.DequeueThought();
		}
	}


}
