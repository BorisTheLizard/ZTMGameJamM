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

        MusicMixer.GetFloat("MusicVolume", out music);
        music = Remap(music, -40f, 10f, 0f, 1f);
        musicSlider.value = music;
        SfxMixer.GetFloat("SFXVolume", out SfxVolume);
        SfxVolume = Remap(SfxVolume, -40f, 10f, 0f, 1f);
        SfxSlider.value = SfxVolume;

        //MUSIC
        //MusicMixer.SetFloat("MusicVolume", Mathf.Log10(music) * 20);
        //musicSlider.value = music;

        //SFX
        //SfxMixer.SetFloat("SFXVolume", Mathf.Log10(SfxVolume) * 20);
        //SfxSlider.value = SfxVolume;
    }


    public void volumeSlider()
    {
        music = Remap(musicSlider.value, 0f, 1f, -40f, 10f);
        MusicMixer.SetFloat("MusicVolume", music);
        //music = musicSlider.value;
        //MusicMixer.SetFloat("MusicVolume", Mathf.Log10(music) * 20);
    }
    public void SfxVolumeSlider()
    {
        SfxVolume = Remap(SfxSlider.value, 0f, 1f, -40f, 10f);
        SfxMixer.SetFloat("SFXVolume", SfxVolume);

        //SfxVolume = SfxSlider.value;
        //SfxMixer.SetFloat("SFXVolume", Mathf.Log10(SfxVolume) * 20);
        //sFXplayer.Play();
    }

    private float Remap(float value, float min1, float max1, float min2, float max2)
    {
        return min2 + (value - min1) * (max2 - min2) / (max1 - min1);
    }
}
