using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireDamage : MonoBehaviour
{
	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "player")
		{
			other.GetComponent<HealthSystem>().TakeDamage(1);
		}
	}
}
