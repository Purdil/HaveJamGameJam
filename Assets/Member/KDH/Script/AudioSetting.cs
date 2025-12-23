using System;
using CSILib.SoundManager.RunTime;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioType
{
    Master,
    BGM,
    SFX
}

public class AudioSetting : MonoSingleton<AudioSetting>
{
    [SerializeField] private AudioMixer audioMixer;

    private void Start()
    {
        VolumeChange(AudioType.Master);
        VolumeChange(AudioType.BGM);
        VolumeChange(AudioType.SFX);
    }
    public void SetMasterVolume(float value)
    {
        SetVolume(AudioType.Master, value);
    }

    public void SetBGMVolume(float value)
    {
        SetVolume(AudioType.BGM, value);
    }

    public void SetSFXVolume(float value)
    {
        SetVolume(AudioType.SFX, value);
    }

    public void SetVolume(AudioType type, float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);

        audioMixer.SetFloat(type.ToString(), Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(type.ToString(), value);
    }
    
    private void VolumeChange(AudioType type)
    {
        float value = PlayerPrefs.GetFloat(type.ToString(), 1f);
        SetVolume(type, value);
    }
}
