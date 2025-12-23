using UnityEngine;
using UnityEngine.Audio;
using CSILib.SoundManager.RunTime;

public enum AudioType
{
    Master,
    BGM,
    SFX
}

public class AudioSetting : MonoSingleton<AudioSetting>
{
    [SerializeField] private AudioMixer audioMixer;

    protected void Awake()
    {
        ApplySavedVolume(AudioType.Master);
        ApplySavedVolume(AudioType.BGM);
        ApplySavedVolume(AudioType.SFX);
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

        audioMixer.SetFloat(type.ToString(), Mathf.Log10(value) * 20f);
        PlayerPrefs.SetFloat(type.ToString(), value);
        PlayerPrefs.Save();
    }

    private void ApplySavedVolume(AudioType type)
    {
        float value = PlayerPrefs.GetFloat(type.ToString(), 1f);
        SetVolume(type, value);
    }
}
