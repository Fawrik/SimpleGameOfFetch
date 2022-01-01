using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicOnStart : MonoBehaviour
{
	public Animator fadeScreen;
	public AudioSource musicSource;
	public AudioClip musicClip;

	public AudioSource credits;
	public AudioClip creditsSFX;

	public void Start()
	{
		StartCoroutine(FadeIn(3f));

	}



	public void PlayCreditsSFX()
	{
		credits.PlayOneShot(creditsSFX);
	}


	public IEnumerator FadeIn(float fadingTime)
	{

		yield return new WaitForSeconds(.2f);
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
