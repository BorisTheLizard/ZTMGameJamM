using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onBushCollision : MonoBehaviour
{
	[SerializeField] Animator anim;
	[SerializeField] ParticleSystem leaves;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "player" || other.tag == "enemy")
		{
			anim.SetTrigger("colide");
			leaves.Play();
		}
	}
}
