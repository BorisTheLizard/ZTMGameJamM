using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textSizeOnEnable : MonoBehaviour
{
	Text text;
	private bool lvlEnd = false;
	public int maxFontSize = 300;

	private void Start()
	{
		text = GetComponent<Text>();
	}
	private void OnEnable()
	{
		lvlEnd = true;
	}
	private void Update()
	{
		if (lvlEnd)
		{
			if(text.fontSize< maxFontSize)
			text.fontSize += 1;
		}
	}
}
