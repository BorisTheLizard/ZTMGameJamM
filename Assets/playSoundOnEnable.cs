using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSoundOnEnable : MonoBehaviour
{
	AudioSource audioSource;
	[SerializeField] AudioClip sound;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}
	private void OnEnable()
	{
		audioSource.PlayOneShot(sound);
	}
}
