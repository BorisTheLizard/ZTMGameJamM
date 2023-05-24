using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bulletsCounterUI : MonoBehaviour
{
	Text counterText;
	AttackSystem bulletsCount;

	private void Start()
	{
		counterText = GetComponent<Text>();
		bulletsCount = FindObjectOfType<AttackSystem>();
	}

	private void Update()
	{
		counterText.text = "X" + bulletsCount.bulletsInClip.ToString();
	}
}
