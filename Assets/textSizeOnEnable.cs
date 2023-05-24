using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textSizeOnEnable : MonoBehaviour
{
	Text text;
	private bool lvlEnd = false;

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
			if(text.fontSize<300)
			text.fontSize += 1;
		}
	}
}
