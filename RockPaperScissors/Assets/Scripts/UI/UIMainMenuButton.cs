using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIMainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [Header("Hover Tweening Settings")]
    [SerializeField, Range(-30.0f, 30.0f)] private float _tiltDegrees;
    [SerializeField, Range(0.0f, 1.0f)] private float _scaleAmount;
    [SerializeField, Range(0.0f, 1.0f)] private float _tweenTime;
    [SerializeField] private LeanTweenType _tweenType;

    private Button _buttonComponent;
    private Vector3 _baseScale;
    private float _baseTilt;

    private void Awake()
    {
        _buttonComponent = GetComponent<Button>();
        _baseScale = gameObject.transform.localScale;
        _baseTilt = gameObject.transform.rotation.eulerAngles.z;
    }

    private void TweenSelection()
    {
        Vector3 targetScale = _baseScale + (_baseScale.normalized * _scaleAmount);

        LeanTween.scale(gameObject, targetScale, _tweenTime).setEase(_tweenType).setIgnoreTimeScale(true);
        LeanTween.rotateZ(gameObject, _tiltDegrees, _tweenTime).setEase(_tweenType).setIgnoreTimeScale(true);
    }

    private void TweenDeselect()
    {
        LeanTween.scale(gameObject, _baseScale, _tweenTime).setEase(_tweenType).setIgnoreTimeScale(true);
        LeanTween.rotateZ(gameObject, _baseTilt, _tweenTime).setEase(_tweenType).setIgnoreTimeScale(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.selectedObject = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.selectedObject = null;
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (_buttonComponent.interactable)
            TweenSelection();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (_buttonComponent.interactable)
            TweenDeselect();
    }
}
