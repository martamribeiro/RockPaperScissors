using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameHandler : MonoBehaviour
{
    public static UIGameHandler Instance { get; private set; }

    [Header("Player1")]
    [SerializeField] private TextMeshProUGUI _player1Name;
    [SerializeField] private TextMeshProUGUI _player1Score;

    [Header("Player2")]
    [SerializeField] private TextMeshProUGUI _player2Name;
    [SerializeField] private TextMeshProUGUI _player2Score;
    [SerializeField] private Transform[] _player2InputGuides;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.StartGame();
    }

    public void ChangePlayerOneName(string playerName)
    {
        ChangePlayerName(_player1Name, playerName);
    }

    public void ChangePlayerTwoName(string playerName)
    {
        ChangePlayerName(_player2Name, playerName);
    }

    private void ChangePlayerName(TextMeshProUGUI playerText, string playerName) 
    {
        playerText.text = playerName;
    }

    public void UpdatePlayerOneScore(int score)
    {
        UpdatePlayerScore(_player1Score, score);
    }

    public void UpdatePlayerTwoScore(int score)
    {
        UpdatePlayerScore(_player2Score, score);
    }

    private void UpdatePlayerScore(TextMeshProUGUI scoreText, int score)
    {
        scoreText.text = score.ToString();
    }

    public void HandlePlayer2InputGuide(bool isComputer)
    {
        foreach (Transform inputGuide in _player2InputGuides)
        {
            if (isComputer)
            {
                inputGuide.gameObject.SetActive(false);
            }
            else
            {
                inputGuide.gameObject.SetActive(true);
            }
        }
    }
}