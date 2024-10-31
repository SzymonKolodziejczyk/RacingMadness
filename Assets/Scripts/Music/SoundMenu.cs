using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider backgroundSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        if(PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetBackgroundVolume();
            SetSFXVolume();
        }
    }

    private void LoadVolume()
    {
        backgroundSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetBackgroundVolume();
        SetSFXVolume();
    }

    public void SetBackgroundVolume()
    {
        float volume = backgroundSlider.value;
        myMixer.SetFloat("BackgroundVolume", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("CarVolume", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void ToggleBackgroundVolume(bool mute)
    {
        float volume = backgroundSlider.value;
        if(mute)
        {
            myMixer.SetFloat("BackgroundVolume", Mathf.Log10(0.0001f)*20);
        }
        else
        {
            myMixer.SetFloat("BackgroundVolume", Mathf.Log10(volume)*20);
        }
    }
}
