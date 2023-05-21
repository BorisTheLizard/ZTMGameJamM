using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightIntencity : MonoBehaviour
{
	Light lightt;
	AudioSource source;
	[SerializeField] float intenseMin = 0;
	[SerializeField] float intenseMax = 10;
	private void Start()
	{
		lightt = GetComponent<Light>();
		source = GetComponent<AudioSource>();
		if (source != null)
		{
			source.Play();
		}
	}
	private void FixedUpdate()
	{
		lightt.intensity = Random.Range(intenseMin, intenseMax);
	}
}
