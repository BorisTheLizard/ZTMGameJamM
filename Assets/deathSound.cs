using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathSound : MonoBehaviour
{
	AudioSource audioSource;
	[SerializeField] AudioClip[] deathSounds;
	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}
	private void OnEnable()
	{
		audioSource.clip = deathSounds[Random.Range(0, deathSounds.Length)];
		audioSource.Play();
	}
}
