using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robotHitColliderLogic : MonoBehaviour
{
	[SerializeField] int damageAmount=1;
	[SerializeField] float timeToOff=0.1f;
	private void OnEnable()
	{
		StartCoroutine(waitToOff());
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "EMP" || other.tag == "player")
		{
			other.GetComponent<HealthSystem>().TakeDamage(damageAmount);
			this.gameObject.SetActive(false);
		}
	}
	IEnumerator waitToOff()
	{
		Debug.Log("called off");
		yield return new WaitForSeconds(timeToOff);
		this.gameObject.SetActive(false);
	}
}
