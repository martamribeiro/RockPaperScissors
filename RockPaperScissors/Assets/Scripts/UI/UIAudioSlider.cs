using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class UIAudioSlider : MonoBehaviour
{
    enum TargetAudio
    {
        Master,
        Music,
        SFX,
    }

    [SerializeField] private TargetAudio _targetAudio;

    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener (delegate { OnSliderValueChanged(); }) ;
    }

    public void OnSliderValueChanged()
    {
        switch (_targetAudio) 
        {
            case TargetAudio.Master:
                AudioController.Instance.ChangeMasterVolume(_slider.value); break;
            case TargetAudio.Music:
                AudioController.Instance.ChangeMusicVolume(_slider.value); break;
            case TargetAudio.SFX:
                AudioController.Instance.ChangeSFXVolume(_slider.value); break;
        }
        
    }
}
