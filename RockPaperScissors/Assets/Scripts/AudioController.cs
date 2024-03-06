using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance {get; private set; }

    private const string MASTER = "Master";
    private const string MUSIC = "Music";
    private const string SFX = "SFX";

    [SerializeField] AudioMixer _audioMixer;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        ChangeMasterVolume(1.0f);
        ChangeMusicVolume(1.0f);
        ChangeSFXVolume(1.0f);

        Instance = this;
    }

    public void ChangeMasterVolume(float sliderValue)
    {
        float volumeValue = CalculateVolumeValue(sliderValue);

        _audioMixer.SetFloat(MASTER, volumeValue);
    }

    public float GetMasterVolumeValue()
    {
        _audioMixer.GetFloat(MASTER, out float volumeValue);
        return volumeValue;
    }

    public void ChangeMusicVolume(float sliderValue)
    {
        float volumeValue = CalculateVolumeValue(sliderValue);

        _audioMixer.SetFloat(MUSIC, volumeValue);
    }

    public float GetMusicVolumeValue()
    {
        _audioMixer.GetFloat(MUSIC, out float volumeValue);
        return volumeValue;
    }

    public void ChangeSFXVolume(float sliderValue)
    {
        float volumeValue = CalculateVolumeValue(sliderValue);

        _audioMixer.SetFloat(SFX, volumeValue);
    }

    public float GetSFXVolumeValue()
    {
        _audioMixer.GetFloat(SFX, out float volumeValue);
        return volumeValue;
    }

    private float CalculateVolumeValue(float sliderValue)
    {
        return Mathf.Log10(sliderValue) * 20;
    }
}
