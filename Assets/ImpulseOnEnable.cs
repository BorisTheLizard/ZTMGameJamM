using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ImpulseOnEnable : MonoBehaviour
{
	[SerializeField]CinemachineImpulseSource source;

	private void Start()
	{
		//source = GetComponent<CinemachineImpulseSource>();
	}
	private void OnEnable()
	{
		source.GenerateImpulse();
	}
}
