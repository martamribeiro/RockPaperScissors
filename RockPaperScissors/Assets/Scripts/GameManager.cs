using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public Action<Hand.Gesture> OnPlayerOneInput;
    public Action<Hand.Gesture> OnPlayerTwoInput;

    public static GameManager Instance { get; private set; }

    bool isOnePlayerMode = false;

    bool player1Chosen = false;
    bool player1Revealed = false;
    Hand.Gesture player1Gesture = Hand.Gesture.None;

    bool player2Chosen = false;
    bool player2Revealed = false;
    Hand.Gesture player2Gesture = Hand.Gesture.None;

    private UIGameHandler _gameUIHandler;

    private int _player1Score = 0;
    private int _player2Score = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        _gameUIHandler = UIGameHandler.Instance;
    }

    void Update()
    {
        HandlePlayerInput();
    }

    public void StartSinglePlayerGame()
    {
        isOnePlayerMode = true;
    }

    public void StartMultiPlayerGame()
    {
        isOnePlayerMode = false;
    }

    public void StartGame()
    {
        _gameUIHandler = UIGameHandler.Instance;

        // Set the player Scores
        _gameUIHandler.UpdatePlayerOneScore(0);
        _player1Score = 0;

        _gameUIHandler.UpdatePlayerTwoScore(0);
        _player2Score = 0;

        player1Chosen = false;
        player2Chosen = false;

        // Set the player Names
        _gameUIHandler.ChangePlayerOneName("Player 1");

        string player2Name;

        if (!isOnePlayerMode)
        {
            player2Name = "Player 2";
            _gameUIHandler.HandlePlayer2InputGuide(false);
        }
        else
        {
            player2Name = "Computer";
            _gameUIHandler.HandlePlayer2InputGuide(true);
        }

        _gameUIHandler.ChangePlayerTwoName(player2Name);
    }

    void HandlePlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.A) && player1Chosen==false)
        {
            OnPlayerOneInput?.Invoke(Hand.Gesture.Rock);
            player1Chosen = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && player1Chosen == false)
        {
            OnPlayerOneInput?.Invoke(Hand.Gesture.Paper);
            player1Chosen = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && player1Chosen == false)
        {
            OnPlayerOneInput?.Invoke(Hand.Gesture.Scissors);
            player1Chosen = true;
        }

        if (Input.GetKeyDown(KeyCode.J) && player2Chosen == false)
        {
            OnPlayerTwoInput?.Invoke(Hand.Gesture.Rock);
            player2Chosen = true;
        }
        else if (Input.GetKeyDown(KeyCode.K) && player2Chosen == false)
        {
            OnPlayerTwoInput?.Invoke(Hand.Gesture.Paper);
            player2Chosen = true;
        }
        else if (Input.GetKeyDown(KeyCode.L) && player2Chosen == false)
        {
            OnPlayerTwoInput?.Invoke(Hand.Gesture.Scissors);
            player2Chosen = true;
        }
    }

    public void HandlePlayerOneReveal(Hand.Gesture gesture)
    {
        player1Revealed = true;
        player1Gesture = gesture;

        if (player2Revealed)
        {
            DetermineWinner();
        }
    }

    public void HandlePlayerTwoReveal(Hand.Gesture gesture)
    {
        player2Revealed = true;
        player2Gesture = gesture;

        if (player1Revealed)
        {
            DetermineWinner();
        }
    }

    void DetermineWinner()
    {
        if (player1Gesture == player2Gesture)
        {
            //tie
        }
        else if ((player1Gesture == Hand.Gesture.Rock && player2Gesture == Hand.Gesture.Scissors) ||
                 (player1Gesture == Hand.Gesture.Paper && player2Gesture == Hand.Gesture.Rock) ||
                 (player1Gesture == Hand.Gesture.Scissors && player2Gesture == Hand.Gesture.Paper))
        {
            //player 1 wins
            _player1Score++;
            _gameUIHandler.UpdatePlayerOneScore(_player1Score);
        }
        else
        {
            //player 2 wins
            _player2Score++;
            _gameUIHandler.UpdatePlayerTwoScore(_player2Score);
        }

        player1Chosen = false;
        player1Revealed = false;

        player2Chosen = false;
        player2Revealed = false;
    }
}
