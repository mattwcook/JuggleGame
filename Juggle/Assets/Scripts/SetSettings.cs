using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class SetSettings : MonoBehaviour
{
    [SerializeField] TMP_Dropdown numBallDropdown;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Image muteButtonImage;
    [SerializeField] Sprite soundIcon;
    [SerializeField] Sprite muteIcon;
    public AudioMixer mixer;
    bool muted = false;
    float maxVolumeLevel = 0f;
    float minVolumeLevel = -20.0f;

    string sfxMixerLabel = "SoundFXVolume";
    string musicMixerLabel = "MusicVolume";
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(SaveKeys.numBalls))
        {
            SettingsScript.maxBalls = PlayerPrefs.GetInt(SaveKeys.numBalls);
            numBallDropdown.SetValueWithoutNotify(SettingsScript.maxBalls - 1);
        }
        else
        {
            SetNumberBalls();
        }

        if (PlayerPrefs.HasKey(SaveKeys.mute) && PlayerPrefs.GetInt(SaveKeys.mute) == 1)
        {
            muted = true;
        }
        else
        {
            muted = false;
        }
        SetAudio();
    }

    public void MuteButtonPressed()
    {
        muted = !muted;
        if (muted) 
        {
            PlayerPrefs.SetInt(SaveKeys.mute, 1);
        }
        else
        {
            PlayerPrefs.SetInt(SaveKeys.mute, 0);
        }
        SetAudio();
    }

    void SetAudio()
    {
        if (muted == true)
        {
            muteButtonImage.sprite = muteIcon;
            SetMusicVolume(0, false);
            SetSoundEffectVolume(0, false);
            musicSlider.SetValueWithoutNotify(musicSlider.minValue);
            sfxSlider.SetValueWithoutNotify(sfxSlider.minValue);
        }
        else
        {
            muteButtonImage.sprite = soundIcon;
            float musicVolume;
            if (PlayerPrefs.HasKey(SaveKeys.musicVolume))
            {
                musicVolume = PlayerPrefs.GetFloat(SaveKeys.musicVolume);
            }
            else
            {
                musicVolume = musicSlider.maxValue;
            }
            SetMusicVolume(musicVolume, false);
            musicSlider.SetValueWithoutNotify(musicVolume);

            float sfxVolume;
            if (PlayerPrefs.HasKey(SaveKeys.soundEffectVolume))
            {
                sfxVolume = PlayerPrefs.GetFloat(SaveKeys.soundEffectVolume);
            }
            else
            {
                sfxVolume = sfxSlider.maxValue;
            }
            SetSoundEffectVolume(sfxVolume, false);
            sfxSlider.SetValueWithoutNotify(sfxVolume);
        }
    }

    public void SetNumberBalls(int numBallsIndex)
    {
        SettingsScript.maxBalls = int.Parse(numBallDropdown.options[numBallsIndex].text);
        PlayerPrefs.SetInt(SaveKeys.numBalls, SettingsScript.maxBalls);
    }
    public void SetNumberBalls()
    {
        SettingsScript.maxBalls = int.Parse(numBallDropdown.options[numBallDropdown.value].text);
        PlayerPrefs.SetInt(SaveKeys.numBalls, SettingsScript.maxBalls);
    }
    public void SetMusicVolume(Single volume)
    {
        SetMusicVolume(volume, true);
    }

    public void SetMusicVolume(float volume, bool save)
    {
        volume = Mathf.Clamp01(volume);
        SettingsScript.musicVolume = volume;
        if (volume > 0)
        {
            mixer.SetFloat(musicMixerLabel, minVolumeLevel + (maxVolumeLevel - minVolumeLevel) * volume);
        }
        else
        {
            mixer.SetFloat(musicMixerLabel, -1000);
        }
        if (save)
        {
            PlayerPrefs.SetFloat(SaveKeys.musicVolume, volume);
        }
    }
    public void SetSoundEffectVolume(Single volume)
    {
        SetSoundEffectVolume(volume, true);
    }
    public void SetSoundEffectVolume(float volume, bool save)
    {
        volume = Mathf.Clamp01(volume);
        SettingsScript.soundEffectVolume = volume;
        Debug.Log("SFX: " + volume);
        if (volume > 0)
        {
            mixer.SetFloat(sfxMixerLabel, minVolumeLevel + (maxVolumeLevel - minVolumeLevel) * volume);
        }
        else
        {
            mixer.SetFloat(sfxMixerLabel, -1000);
        }
        if (save)
        {
            PlayerPrefs.SetFloat(SaveKeys.soundEffectVolume, volume);
        }
    }
}
