using System.Collections;
using UnityEngine;

public class onenableDisable : MonoBehaviour
{
	[SerializeField] float timeToOff = 2f;
	private void OnEnable()
	{
		StartCoroutine(DisableText());
	}
	private IEnumerator DisableText()
	{
		yield return new WaitForSeconds(timeToOff);
		this.gameObject.SetActive(false);
	}
}
