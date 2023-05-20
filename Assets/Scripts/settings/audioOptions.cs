using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class audioOptions : MonoBehaviour
{
    [Header("mixers")]
    [SerializeField] AudioMixer MusicMixer;
    //[SerializeField] AudioMixer SfxMixer;

    [Header("sliders")]
    [SerializeField] Slider musicSlider;
    //[SerializeField] Slider SfxSlider;

    //public float music;
    //public float SfxVolume;

    public void Start()
    {
        //MUSIC
        MusicMixer.SetFloat("music", PlayerPrefs.GetFloat("music"));
        musicSlider.value = PlayerPrefs.GetFloat("music");

        //SFX
        //SfxMixer.SetFloat("SfxVolume", PlayerPrefs.GetFloat("SfxVolume"));
        //SfxSlider.value = PlayerPrefs.GetFloat("SfxVolume");
    }

	public void volumeSlider(float sliderValue)
    {
        MusicMixer.SetFloat("music", sliderValue);
        PlayerPrefs.SetFloat("music", sliderValue);
        PlayerPrefs.Save();
    }
/*	public void SfxVolumeSlider(float SfxSliderValue)
	{
		SfxMixer.SetFloat("SfxVolume", SfxSliderValue);
		PlayerPrefs.SetFloat("SfxVolume", SfxSliderValue);
		PlayerPrefs.Save();
	}*/
}
