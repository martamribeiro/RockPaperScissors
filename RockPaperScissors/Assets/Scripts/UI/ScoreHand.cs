using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHand : MonoBehaviour
{
    [SerializeField] private Hand _syncingHand;
    [SerializeField] private Image _handImage;
    [SerializeField] private Sprite[] _handSprites;

    [SerializeField] private Sprite _winningSprite;

    [Header("Label")]
    [SerializeField] private GameObject _label;
    [SerializeField] private TextMeshProUGUI _labelText;

    private int _currentHandIdx = 0;

    private void Start()
    {
        _syncingHand.OnMoveComplete += ChangeHandSprite;
        GameManager.Instance.OnPlayerWin += ShowPlayerWin;
    }

    private void OnDisable()
    {
        _syncingHand.OnMoveComplete -= ChangeHandSprite;
        GameManager.Instance.OnPlayerWin -= ShowPlayerWin;
    }

    private void OnDestroy()
    {
        _syncingHand.OnMoveComplete -= ChangeHandSprite;
        GameManager.Instance.OnPlayerWin -= ShowPlayerWin;
    }

    private void ChangeHandSprite() 
    {
        _currentHandIdx++;

        if (_currentHandIdx >= _handSprites.Length)
            _currentHandIdx = 0;

        _handImage.sprite = _handSprites[_currentHandIdx];
    }

    private void ShowPlayerWin(Hand.Player winningPlayer) 
    {
        float rotation = 0.0f;
        string winningText = "Tie!";

        if (winningPlayer == Hand.Player.Player1)
        {
            rotation = 90.0f;
            winningText = string.Format("Player 1\nWins!");
        }
        else if (winningPlayer == Hand.Player.Player2)
        {
            rotation = -90.0f;

            if (GameManager.Instance.isOnePlayerMode)
            {
                winningText = string.Format("Computer\nWins!");
            }
            else
            {
                winningText = string.Format("Player 2\nWins!");
            }
        }

        _handImage.sprite = _winningSprite;
        _currentHandIdx = 0;

        _label.SetActive(true);
        _label.transform.localScale = Vector3.zero;
        LeanTween.scale(_label, Vector3.one, 0.5f).setEaseInOutSine();

        _labelText.text = winningText;
        LeanTween.rotateZ(gameObject, rotation, 0.5f).setEaseOutCirc().setOnComplete(TweenToDefaultPosition);
    }

    private void TweenToDefaultPosition()
    {
        LeanTween.rotateZ(gameObject, 0.0f, 0.5f).setEaseInCirc().setOnComplete(HideLabelTween);
        _handImage.sprite = _handSprites[0];
        
    }

    private void HideLabelTween()
    {
        LeanTween.scale(_label, Vector3.zero, 0.3f).setEaseInOutSine().setOnComplete(DeactivateLabel);
    }

    private void DeactivateLabel()
    {
        _label.SetActive(false);
    }
}
