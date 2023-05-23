using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupBullets : MonoBehaviour
{
	public int amount = 3;
	GameObjectPool pool;
	private void Start()
	{
		pool = FindObjectOfType<GameObjectPool>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "player")
		{
			other.GetComponent<AttackSystem>().bulletsInClip += amount;
			this.gameObject.SetActive(false);
			pool.ReturnObject(this.gameObject);
		}
	}
}
