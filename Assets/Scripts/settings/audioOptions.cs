using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class audioOptions : MonoBehaviour
{
    [Header("mixers")]
    [SerializeField] AudioMixer MusicMixer;
    [SerializeField] AudioMixer SfxMixer;

    [Header("sliders")]
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SfxSlider;

    public float music;
    public float SfxVolume;

    //[Header("AudioSources")]
    //[SerializeField] AudioSource sFXplayer;

    public void Start()
    {
        //MUSIC
        MusicMixer.SetFloat("MusicVolume", Mathf.Log10(music) * 20);
        musicSlider.value = music;

        //SFX
        SfxMixer.SetFloat("SFXVolume", Mathf.Log10(SfxVolume) * 20);
        SfxSlider.value = SfxVolume;
    }


    public void volumeSlider()
    {
        music = musicSlider.value;
        MusicMixer.SetFloat("MusicVolume", Mathf.Log10(music) * 20);
    }
    public void SfxVolumeSlider()
    {
        SfxVolume = SfxSlider.value;
        SfxMixer.SetFloat("SFXVolume", Mathf.Log10(SfxVolume) * 20);
        //sFXplayer.Play();
    }
}
