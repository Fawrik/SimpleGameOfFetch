using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
	public AudioSource audioSource;
	public AudioClip walk;
	public AudioClip blanket;
	public AudioClip coffee;

	public AudioClip uiSelect1;
	public AudioClip uiSelect2;

	public GameObject blackScreen;

	public PlayMusicOnStart p;

	public Animator fadeScreen;

	public void PlayGame()
	{
		StartCoroutine(StartGame());
		audioSource.PlayOneShot(uiSelect1);
	}

	public void QuitGame()
	{
		StartCoroutine(QuitGamee());
	}

	public IEnumerator StartGame()
	{
		audioSource.PlayOneShot(uiSelect1);
		yield return new WaitForSeconds(0.3f);

		p.musicSource.Stop();

		fadeScreen.SetInteger("fader", 1);

		yield return new WaitForSeconds(1.8f);

		SceneManager.LoadScene("Game");
	}

	public void PlayUISound()
	{
		audioSource.PlayOneShot(uiSelect1);
		print("yesh");
	}

	public IEnumerator QuitGamee()
	{
		audioSource.PlayOneShot(uiSelect2);
		yield return new WaitForSeconds(1);
		Application.Quit();
	}
}
