using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
	[SerializeField] Animator anim;
	float shootingSpeed;
	[SerializeField] float maxShootingSpeed = 0.5f;
	[SerializeField] GameObject hitCollider;
	GameObjectPool pool;
	[SerializeField] int objectIndex;
	public int bulletsInClip = 5;
	[SerializeField] Transform shootingPoint;
	[SerializeField] ParticleSystem slash;
	AudioSource audioSource;
	[SerializeField] AudioClip click;
	[SerializeField] AudioClip Shoot;
	[SerializeField] AudioClip meleAttackClip;

	private void Start()
	{
		pool = FindObjectOfType<GameObjectPool>();
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			shooting();
		}
		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			StartCoroutine(meleAttack());
		}
	}
	private void shooting()
	{
		if (Time.time > shootingSpeed)
		{
			if (bulletsInClip > 0)
			{
				shootingSpeed = Time.time + maxShootingSpeed;
				audioSource.PlayOneShot(Shoot);
				GameObject obj = pool.GetObject(objectIndex);
				if (obj != null)
				{
					obj.SetActive(false);
					obj.transform.position = shootingPoint.position;
					obj.transform.rotation = shootingPoint.rotation;
					obj.SetActive(true);
				}
				bulletsInClip--;
			}
			else
			{
				audioSource.PlayOneShot(click);
			}
		}
	}
	IEnumerator meleAttack()
	{
		slash.Play();
		audioSource.PlayOneShot(meleAttackClip);
		hitCollider.SetActive(true);
		yield return new WaitForSeconds(0.1f);
		hitCollider.SetActive(false);
	}
}
