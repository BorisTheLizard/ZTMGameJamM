using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class meleHitCollider : MonoBehaviour
{
	[SerializeField] int damageAmount;
	[SerializeField] int HitImpactIndex;
	GameObjectPool pool;
	CinemachineImpulseSource source;

	private void Start()
	{
		pool = FindObjectOfType<GameObjectPool>();
		source = GetComponent<CinemachineImpulseSource>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "enemy")
		{
			other.GetComponent<HealthSystem>().TakeDamage(damageAmount);
			GameObject hitImpact = pool.GetObject(HitImpactIndex);
			source.GenerateImpulse();
			if (hitImpact != null)
			{
				hitImpact.SetActive(false);
				hitImpact.transform.position = transform.position;
				hitImpact.transform.rotation = transform.rotation;
				hitImpact.SetActive(true);
			}
		}
		if (other.tag == "obstacles")
		{
			GameObject hitImpact = pool.GetObject(HitImpactIndex);
			source.GenerateImpulse();
			if (hitImpact != null)
			{
				hitImpact.SetActive(false);
				hitImpact.transform.position = transform.position;
				hitImpact.transform.rotation = transform.rotation;
				hitImpact.SetActive(true);
			}
		}
	}
}
