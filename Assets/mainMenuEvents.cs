using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuEvents : MonoBehaviour
{
	AudioSource audioSource;
	[SerializeField] AudioClip onPointerSound;
	[SerializeField] GameObject NewGameTimeLine;
	[SerializeField] GameObject optionsMenu;
	[SerializeField] GameObject mainMenu;

	[SerializeField] GameObject credits;
	[SerializeField] GameObject returnFromCredits;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}
	public void onPointerEnterSound()
	{
		if (!audioSource.isPlaying)
		{
			audioSource.PlayOneShot(onPointerSound);
		}
	}
	public void StartGame()
	{
		NewGameTimeLine.SetActive(true);
	}
	public void LaunchLvl1()
	{
		SceneManager.LoadScene(1);
	}
	public void CallOptions()
	{
		optionsMenu.SetActive(true);
		mainMenu.SetActive(false);
	}
	public void returnToMainMenu()
	{
		optionsMenu.SetActive(!true);
		mainMenu.SetActive(!false);
	}

	public void callCredits()
	{
		credits.SetActive(true);
		mainMenu.SetActive(false);
		returnFromCredits.SetActive(true);
	}

	public void closeCredits()
	{
		credits.SetActive(!true);
		mainMenu.SetActive(!false);
		returnFromCredits.SetActive(!true);
	}
}
