using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalScreen : MonoBehaviour
{
	[SerializeField] float secondsToWait = 10f;
	[SerializeField] AudioSource audioSource;
	[SerializeField] Animator anim;
	[SerializeField] GameObject credits;
	[SerializeField] GameObject skip;
	
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(secondsToWait);
		this.gameObject.SetActive(false);
		audioSource.Play();
		credits.SetActive(true);
		skip.SetActive(true);
		anim.SetTrigger("dance");
	}
}
