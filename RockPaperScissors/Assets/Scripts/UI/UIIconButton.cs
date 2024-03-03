using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIIconButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Transform _buttonIcon;

    [Header("Hover Tweening Settings")]
    [SerializeField, Range(0.0f, 1.0f)] private float _scaleAmount;
    [SerializeField, Range(0.0f, 1.0f)] private float _tweenTime;
    [SerializeField] private LeanTweenType _tweenType;

    private Button _buttonComponent;
    private Vector3 _baseScale;

    private void Awake()
    {
        _buttonComponent = GetComponent<Button>();
        _baseScale = _buttonIcon.gameObject.transform.localScale;
    }

    private void TweenSelection()
    {
        Vector3 targetScale = _baseScale + (_baseScale.normalized * _scaleAmount);

        LeanTween.scale(_buttonIcon.gameObject, targetScale, _tweenTime).setEase(_tweenType).setIgnoreTimeScale(true);
    }

    private void TweenDeselect()
    {
        LeanTween.scale(_buttonIcon.gameObject, _baseScale, _tweenTime).setEase(_tweenType).setIgnoreTimeScale(true);
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
