using System.Collections;
using UnityEngine;

public class onenableDisable : MonoBehaviour
{
	private void OnEnable()
	{
		StartCoroutine(DisableText());
	}
	private IEnumerator DisableText()
	{
		yield return new WaitForSeconds(2);
		this.gameObject.SetActive(false);
	}
}
