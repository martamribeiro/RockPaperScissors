using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBackground : MonoBehaviour
{
    private int _pulseCount;

    private void Awake()
    {
        _pulseCount = 0;
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
