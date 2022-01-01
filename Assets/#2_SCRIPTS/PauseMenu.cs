using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

	public static PauseMenu Instance;
	private void Awake()
	{
		if (Instance == null)
			Instance = this;
	}

	public AudioSource audioSource;
	public AudioSource menu;
	public AudioClip callDog;
	public AudioClip dogResponse;
	public AudioClip PackAndLeave;
	public AudioClip walkAway;

	public GameObject blackScreen;

	public GameStartScript p;

	public AudioSource ball;
	public AudioSource man;
	public AudioSource dog;

	public AudioClip ui1;
	public AudioClip ui2;
	public AudioClip openUI;

	public bool gameIsPaused = false;

	public GameObject pauseMenuUI;

	public GameObject pauseUI;

	public bool canOpenMenu = true;

	public void ExitToMainMenu()
	{
		canOpenMenu = false;
		StartCoroutine(GameStartScript.Instance.LeaveToMainMenu());
	}



	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && canOpenMenu)
		{
			if (!gameIsPaused)
				PauseGame();
			else
				ResumeGame();
		}
	}

	public void PauseGame()
	{
		menu.PlayOneShot(openUI);
		p.musicSource.volume = 0.25f;
		pauseMenuUI.SetActive(true);
		gameIsPaused = true;
		Time.timeScale = 0;
		pauseUI.SetActive(false);
	}
	public void ResumeGame()
	{
		menu.PlayOneShot(ui1);
		p.musicSource.volume = 1f;
		pauseMenuUI.SetActive(false);
		gameIsPaused = false;
		Time.timeScale = 1;
		pauseUI.SetActive(true);

	}

	public IEnumerator LeaveToMainMenu()
	{
		
		menu.PlayOneShot(ui2);
		p.musicSource.Stop();
		man.Stop();
		ball.Stop();
		dog.Stop();

		//add animation
		blackScreen.GetComponent<SpriteRenderer>().enabled = true;

		Time.timeScale = 1;
		//Sfx man enter
		audioSource.PlayOneShot(callDog);
		yield return new WaitForSeconds(1);

		//Sfx blanket
		audioSource.PlayOneShot(dogResponse);
		yield return new WaitForSeconds(1.2f);

		//Sfx coffee
		audioSource.PlayOneShot(PackAndLeave);
		yield return new WaitForSeconds(4f);

		audioSource.PlayOneShot(walkAway);
		yield return new WaitForSeconds(4f);

		SceneManager.LoadScene("Start Menu");
	}
}
