using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class resolutionSettings : MonoBehaviour
{
	[SerializeField] TMP_Dropdown resolutionDropDown;

	private Resolution[] resolutions;
	private List<Resolution> filteredResolutions;

	private float currentRefreshRate;
	private int currentResolutionIndex = 0;

	private void Start()
	{
		resolutions = Screen.resolutions;
		filteredResolutions = new List<Resolution>();

		resolutionDropDown.ClearOptions();
		currentRefreshRate = Screen.currentResolution.refreshRate;

		for (int i = 0; i < resolutions.Length; i++)
		{
			if (resolutions[i].refreshRate == currentRefreshRate)
			{
				filteredResolutions.Add(resolutions[i]);
			}
		}

		List<string> options = new List<string>();
		for (int i = 0; i < filteredResolutions.Count; i++)
		{
			string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height/* + " " + filteredResolutions[i].refreshRate + "Hz"*/;
			options.Add(resolutionOption);
			if(filteredResolutions[i].width==Screen.width && filteredResolutions[i].height == Screen.height)
			{
				currentResolutionIndex = i;
			}
		}

		resolutionDropDown.AddOptions(options);
		resolutionDropDown.value = currentResolutionIndex;
		resolutionDropDown.RefreshShownValue();
	}

	public void SetREsolution (int resolutionIndex)
	{
		Resolution resolution = filteredResolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, true);
	}
}
