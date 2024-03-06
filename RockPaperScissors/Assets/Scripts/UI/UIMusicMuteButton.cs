using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIMusicMuteButton : MonoBehaviour
{
    [SerializeField] private Image _buttonIcon;
    [SerializeField] private Sprite _mutedSprite;
    [SerializeField] private Sprite _defaultSprite;
    
    private Button _buttonComponent;
    private bool _isMuted = false;
    private float _previousVolume;

    private void Start()
    {
        _buttonComponent = GetComponent<Button>();

        /*_previousVolume = AudioController.Instance.GetMusicVolumeValue();

        if (_previousVolume < 0.01f)
        {
            _previousVolume = 1.0f;
            _buttonIcon.sprite = _mutedSprite;
        }*/

        _buttonComponent.onClick.AddListener(HandleMuteMusic);
    }

    private void HandleMuteMusic()
    {
        Debug.Log("here");
        AudioController audioController = AudioController.Instance;

        if (!_isMuted)
        {
            audioController.ChangeMusicVolume(0.001f);
            _buttonIcon.sprite = _mutedSprite;
            _isMuted = true;
        }
        else
        {
            audioController.ChangeMusicVolume(1.0f);
            _buttonIcon.sprite = _defaultSprite;
            _isMuted = false;
        }

    }
}
