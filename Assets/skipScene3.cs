using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skipScene3 : MonoBehaviour
{
	[SerializeField] GameObject cutscene;
	[SerializeField] GameObject activeGameplayElements;
	[SerializeField] GameObject GameplayElements;
	[SerializeField] GameObject button;

	public void skipCutScene()
	{
		cutscene.SetActive(false);
		activeGameplayElements.SetActive(true);
		GameplayElements.SetActive(true);
		button.SetActive(false);
	}
}
