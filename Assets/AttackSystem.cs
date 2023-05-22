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
	[SerializeField] int bullets;
	[SerializeField] Transform shootingPoint;
	[SerializeField] ParticleSystem slash;
	AudioSource audioSource;
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
			shootingSpeed = Time.time + maxShootingSpeed;
			GameObject obj = pool.GetObject(bullets);
			if (obj != null)
			{
				obj.SetActive(false);
				obj.transform.position = shootingPoint.position;
				obj.transform.rotation = shootingPoint.rotation;
				obj.SetActive(true);
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
