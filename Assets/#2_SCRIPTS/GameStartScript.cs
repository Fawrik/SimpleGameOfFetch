using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartScript : MonoBehaviour
{

	public static GameStartScript Instance;
	private void Awake()
	{
		if (Instance == null)
			Instance = this;
	}

	public bool ignoreIntro;



	public AudioSource musicSource;
	public AudioClip musicClip;

	public Animator fadeScreen;

	public AudioSource manVoice;
	public AudioSource footSteps;
	public AudioSource dog;
	public AudioSource action1;
	public AudioSource action2;

	public AudioClip manCall1;
	public AudioClip manCall2;
	public AudioClip dogEnter;
	public AudioClip dogLeave;
	public AudioClip footStepsIn;
	public AudioClip footStepsOut;
	public AudioClip packingUp;
	public AudioClip packingOut;
	public AudioClip walkingAway;



	public void Start()
	{
		if (!ignoreIntro)
		{
			StartCoroutine(IntroStart());
		}
		else
		{
			fadeScreen.SetInteger("fader", 0);
			musicSource.loop = true;
			musicSource.clip = musicClip;
			musicSource.Play();
			Man.Instance.canThrow = true;
		}

	}



	public IEnumerator IntroStart()
	{
		Man.Instance.canThrow = false;
		fadeScreen.SetInteger("fader", 1);

		PauseMenu.Instance.menu.volume = 0;
		PauseMenu.Instance.PauseGame();
		PauseMenu.Instance.ResumeGame();
		

		yield return new WaitForSeconds(.1f);     //mIGHT need to check in on this if builds falls apart

		action1.PlayOneShot(footStepsIn);
		yield return new WaitForSeconds(3.5f);
		PauseMenu.Instance.menu.volume = 1;

		dog.PlayOneShot(dogEnter);
		yield return new WaitForSeconds(1.1f);

		manVoice.PlayOneShot(manCall1);
		yield return new WaitForSeconds(1.2f);

		action2.PlayOneShot(packingOut);
		yield return new WaitForSeconds(1.5f);

		fadeScreen.SetInteger("fader", 0);

		musicSource.loop = true;
		musicSource.clip = musicClip;
		//musicSource.Play();
		StartCoroutine(FadeIn(3.5f));

		yield return new WaitForSeconds(1.5f);

		Man.Instance.canThrow = true;
		//yield return new WaitForSeconds(.5f);

		TutorialScript.Instance.SetTutorialActive();
	}


	public IEnumerator LeaveToMainMenu()
	{
		PauseMenu.Instance.menu.PlayOneShot(PauseMenu.Instance.ui2);
		musicSource.Stop();
		manVoice.Stop();
		footSteps.Stop();
		dog.Stop();
		Time.timeScale = 1;

		//add animation
		fadeScreen.SetInteger("fader", 1);

		yield return new WaitForSeconds(1.9f);

		

		//Sfx man enter
		manVoice.PlayOneShot(manCall2);
		yield return new WaitForSeconds(1);

		//Sfx blanket
		dog.PlayOneShot(dogLeave);
		yield return new WaitForSeconds(1.2f);

		//Sfx coffee
		manVoice.PlayOneShot(packingUp);
		yield return new WaitForSeconds(4f);

		//dog.PlayOneShot(walkingAway);
		//yield return new WaitForSeconds(4f);

		SceneManager.LoadScene("Start Menu");
	}



	public IEnumerator FadeIn(float fadingTime)
	{
		musicSource.loop = true;
		musicSource.clip = musicClip;
		

		musicSource.loop = true;
		musicSource.clip = musicClip;
		musicSource.Play();

		float resultVolume = musicSource.volume;
		float frameCount = fadingTime / Time.deltaTime;
		float framesPassed = 0;

		while (framesPassed <= frameCount)
		{
			var t = framesPassed++ / frameCount;
			musicSource.volume = Mathf.Lerp(0, resultVolume, t);
			yield return null;
		}

		musicSource.volume = resultVolume;
	}
}
