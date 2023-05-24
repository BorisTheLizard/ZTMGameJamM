using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class endLevelTrigger : MonoBehaviour
{
	[SerializeField] float startNextLvlTime = 3f;
	AudioSource audioSource;
	[SerializeField] AudioClip WaveSound;
	[SerializeField] GameObject shockWaveFloor;
	AImanager manager;
	spawner Spawner;
	CinemachineImpulseSource source;
	[SerializeField] GameObject chargedParts;
	[SerializeField] GameObject chargedBurst;
	[SerializeField] GameObject endLvlDarkScreen;
	[SerializeField] GameObject empText;
	//off music
	[SerializeField] CombatMusic combatMusic;
	[SerializeField] GameObject combatMusicObj;


	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		manager = FindObjectOfType<AImanager>();
		source = GetComponent<CinemachineImpulseSource>();
		Spawner = FindObjectOfType<spawner>();
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "projectiles")
		{
			StartCoroutine(LoadNextLevel());
			audioSource.PlayOneShot(WaveSound);
			shockWaveFloor.SetActive(true);
			Spawner.enabled = false;
			chargedParts.SetActive(false);
			chargedBurst.SetActive(true);
			StartCoroutine(turnOffFloor());
			killRobots();
			source.GenerateImpulse();
			combatMusic.enabled = false;
			combatMusicObj.SetActive(false);
			endLvlDarkScreen.SetActive(true);
			empText.SetActive(false);
			this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
			this.gameObject.GetComponent<BoxCollider>().enabled = false;
		}
	}
	IEnumerator LoadNextLevel()
	{
		yield return new WaitForSeconds(startNextLvlTime);
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int nextSceneIndex = currentSceneIndex + 1;
		SceneManager.LoadScene(nextSceneIndex);
	}
	IEnumerator turnOffFloor()
	{
		yield return new WaitForSeconds(0.8f);
		shockWaveFloor.SetActive(false);
	}
	void killRobots()
	{
		foreach (enemyAI robot in manager.Units)
		{
			robot.gameObject.GetComponent<HealthSystem>().TakeDamage(10);
		}
		manager.enabled = false;
	}
}
