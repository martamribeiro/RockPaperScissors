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

        Instance = this;
    }

    public void ChangeMasterVolume(float sliderValue)
    {
        float volumeValue = CalculateVolumeValue(sliderValue);

        _audioMixer.SetFloat(MASTER, volumeValue);
    }

    public void ChangeMusicVolume(float sliderValue)
    {
        float volumeValue = CalculateVolumeValue(sliderValue);

        _audioMixer.SetFloat(MUSIC, volumeValue);
    }

    public void ChangeSFXVolume(float sliderValue)
    {
        float volumeValue = CalculateVolumeValue(sliderValue);

        _audioMixer.SetFloat(SFX, volumeValue);
    }

    private float CalculateVolumeValue(float sliderValue)
    {
        return Mathf.Log10(sliderValue) * 20;
    }
}
