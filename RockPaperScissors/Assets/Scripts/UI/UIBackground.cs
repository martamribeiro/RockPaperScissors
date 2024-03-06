using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBackground : MonoBehaviour
{
    private int _pulseCount;
    private bool isConnected = false;

    private void Awake()
    {
        _pulseCount = 0;
    }

    private void Start()
    {
        if (!isConnected && gameObject.activeSelf)
        {
            MusicController.Instance.OnBeat += Pulse;
            isConnected = true;
        }
    }

    private void OnEnable()
    {
        if (!isConnected && MusicController.Instance != null)
        {
            MusicController.Instance.OnBeat += Pulse;
            _pulseCount = 0;
            isConnected = true;
        }
    }

    private void OnDisable()
    {
        MusicController.Instance.OnBeat -= Pulse;
        isConnected = false;
        _pulseCount = 0;
    }

    private void OnDestroy()
    {
        MusicController.Instance.OnBeat -= Pulse;
    }

    public void Pulse()
    {
        switch (_pulseCount)
        {
            case 0:
                LeanTween.moveLocalX(gameObject, 30.0f, .1f).setEaseInOutSine().setIgnoreTimeScale(true);
                break;
            case 1:
                LeanTween.moveLocalY(gameObject, 30.0f, .1f).setEaseInOutSine().setIgnoreTimeScale(true);
                break;
            case 2:
                LeanTween.moveLocalX(gameObject, 0.0f, .1f).setEaseInOutSine().setIgnoreTimeScale(true);
                break;
            case 3:
                LeanTween.moveLocalY(gameObject, 0.0f, .1f).setEaseInOutSine().setIgnoreTimeScale(true);
                break;
        }

        if (_pulseCount == 3)
        {
            _pulseCount = 0;
        }
        else
        {
            _pulseCount++;
        }
    }
}
