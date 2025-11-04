
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;
    //[SerializeField] private AudioMixer sfxAudioMixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    public void SetBGMVolume()
    {
        float volume = bgmSlider.value;
        audioMixer.SetFloat("bgm",Mathf.Log10(volume)*20);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
    }

    void Start()
    {
        audioMixer.SetFloat("bgm", Mathf.Log10(0.5f) * 20);
        audioMixer.SetFloat("sfx", Mathf.Log10(0.5f) * 20);

        SetBGMVolume();
        SetSFXVolume();
    }


}
