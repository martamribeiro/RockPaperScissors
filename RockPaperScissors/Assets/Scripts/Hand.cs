using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    public enum Gesture
    {
        Rock,
        Paper,
        Scissors,
        None
    }

    public enum Player 
    {
        Player1,
        Player2,
    }

    [SerializeField] Image _handImage;

    [Serializable]
    private struct HandSprites
    {
        public Gesture handGesture;
        public Sprite handSprite;
    }

    [SerializeField] private HandSprites[] _handSprites;
    [SerializeField] private Player _player;

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

    private Gesture _chosenGesture;
    private Dictionary<Gesture, Sprite> _handDictionary;

    private bool _wasGestureChosen = false;

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

        _handDictionary = new Dictionary<Gesture, Sprite>
        {
            { _handSprites[0].handGesture, _handSprites[0].handSprite},
            { _handSprites[1].handGesture, _handSprites[1].handSprite},
            { _handSprites[2].handGesture, _handSprites[2].handSprite}
        };

        _chosenGesture = Gesture.None;
    }

    private void OnEnable()
    {
        TweenUp();

        if (_player == Player.Player1)
        {
            GameManager.Instance.OnPlayerOneInput += ChooseGesture;
        }
        else
        {
            GameManager.Instance.OnPlayerTwoInput += ChooseGesture;
        }
    }

    private void OnDisable()
    {
        if (_player == Player.Player1)
        {
            GameManager.Instance.OnPlayerOneInput -= ChooseGesture;
        }
        else
        {
            GameManager.Instance.OnPlayerTwoInput -= ChooseGesture;
        }
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
        if (_chosenGesture == Gesture.None)
        {
            _chosenGesture = (Gesture)UnityEngine.Random.Range(0, 2);
            _wasGestureChosen = true;
        }

        LeanTween.moveLocalY(gameObject, 0.0f, _revealTime).setEaseInQuint().setOnComplete(RevealChosenGesture);
        LeanTween.moveLocalX(gameObject, _basePosition + _revealMove, _revealTime).setEaseInQuint();
        LeanTween.rotateZ(gameObject, _revealRotation, _revealTime).setEaseInQuint();
    }

    public void ChooseGesture(Gesture gesture)
    {
        if (_wasGestureChosen) return;

        _chosenGesture = gesture;
        _wasGestureChosen = true;
    }

    private void RevealChosenGesture()
    {
        _handImage.sprite = _handDictionary[_chosenGesture];

        if (_player == Player.Player1)
        {
            GameManager.Instance.HandlePlayerOneReveal(_chosenGesture);
        }
        else
        {
            GameManager.Instance.HandlePlayerTwoReveal(_chosenGesture);
        }

        StartCoroutine(nameof(WaitAndReset));
    }

    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(1.0f);

        LeanTween.moveLocalX(gameObject, _basePosition, .5f).setEaseInOutSine();
        
        _handImage.sprite = _handDictionary[Gesture.Rock];
        _chosenGesture = Gesture.None;
        _moveCount = 0;
        _wasGestureChosen = false;
        TweenUp();
    }
}
