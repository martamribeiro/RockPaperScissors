using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    [SerializeField] Image _handImage;
    [SerializeField] Sprite _tmp;
    [SerializeField] Sprite _tmp2;

    [Header("Tween Options")]
    [SerializeField] float _rotationAmount = 15.0f;
    [SerializeField] float _rotationTime = .5f;

    [Space()]
    [SerializeField] float _moveAmount = 30.0f;
    [SerializeField] float _moveTime = .3f;

    [Space()]
    [SerializeField] float _revealTime = .2f;
    [SerializeField] float _revealRotation = 90.0f;
    [SerializeField] float _revealMove = 25.0f;

    private float _baseRotation;
    private float _basePosition;
    private Vector3 _baseScale;

    int _moveCount = 0;

    private void Awake()
    {
        _baseRotation = transform.rotation.eulerAngles.z;
        _basePosition = transform.localPosition.x;
        _baseScale = transform.localScale;

        if (_baseRotation > 180)
        {
            _rotationAmount = -(_rotationAmount);
        }
    }

    private void Start()
    {
        TweenUp();
    }

    private void TweenUp()
    {
        if (_moveCount < 3) 
            LeanTween.rotateZ(gameObject, _baseRotation - _rotationAmount, _rotationTime).setEaseInOutSine().setOnComplete(TweenDown);
        else
            LeanTween.rotateZ(gameObject, _baseRotation - _rotationAmount, _rotationTime).setEaseInOutSine().setOnComplete(TweenReveal);

        LeanTween.moveLocalY(gameObject, _moveAmount, _moveTime).setEaseInOutSine();
    }

    private void TweenDown()
    {
        LeanTween.rotateZ(gameObject, _baseRotation + _rotationAmount, _rotationTime).setEaseInOutSine().setOnComplete(TweenUp);
        LeanTween.moveLocalY(gameObject, -(_moveAmount), _moveTime).setEaseInOutSine();
        _moveCount++;
    }

    private void TweenReveal()
    {
        LeanTween.moveLocalY(gameObject, 0.0f, _revealTime).setEaseInQuint().setOnComplete(ChangeSprite);
        LeanTween.moveLocalX(gameObject, _basePosition + _revealMove, _revealTime).setEaseInQuint();
        LeanTween.rotateZ(gameObject, _revealRotation, _revealTime).setEaseInQuint();
    }

    private void ChangeSprite()
    {
        _handImage.sprite = _tmp;

        StartCoroutine(nameof(WaitAndReset));
    }

    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(1.0f);

        LeanTween.moveLocalX(gameObject, _basePosition, .5f).setEaseInOutSine();
        
        _handImage.sprite = _tmp2;
        _moveCount = 0;
        TweenUp();
    }
}
