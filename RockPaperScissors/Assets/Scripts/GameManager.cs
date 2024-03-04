using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Sprite[] gestures; //order: rock, paper, scissors

    public GameObject player1Gesture, player2Gesture;

    bool isOnePlayerMode = false;
    bool player1Chosen = false;
    bool player2Chosen = false;
    bool winnerDetermined = false;

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

        //SetAlpha(player1Gesture, 0);
        //SetAlpha(player2Gesture, 0);
    }
    void Update()
    {
        if (player1Chosen && player2Chosen && !winnerDetermined)
        {
            DetermineWinner();
            StartCoroutine(StartNewRound());
        }

        if (!isOnePlayerMode)
        {
            // Two-player mode
            HandleTwoPlayerInput();
        }
        else
        {
            // One-player mode
            HandleOnePlayerInput();
        }
    }

    void SetAlpha(GameObject emptySprite, float alpha)
    {
        var image = emptySprite.GetComponent<Image>();
        var color = image.color;
        color.a = alpha;
        image.color = color;
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
        //SetAlpha(player1Gesture, 0);
        //SetAlpha(player2Gesture, 0);

        // Set the player Scores
        _gameUIHandler.UpdatePlayerOneScore(0);
        _gameUIHandler.UpdatePlayerTwoScore(0);

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

    IEnumerator StartNewRound()
    {
        yield return new WaitForSeconds(2f); // Adjust the delay time as needed

        SetAlpha(player1Gesture, 0);
        SetAlpha(player2Gesture, 0);

        player1Chosen = false;
        player2Chosen = false;
        winnerDetermined = false;
    }

    void HandleTwoPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.A) && player1Chosen==false)
        {
            ChooseGesture(player1Gesture, gestures[0]);
            player1Chosen = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && player1Chosen == false)
        {
            ChooseGesture(player1Gesture, gestures[1]);
            player1Chosen = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && player1Chosen == false)
        {
            ChooseGesture(player1Gesture, gestures[2]);
            player1Chosen = true;
        }

        if (Input.GetKeyDown(KeyCode.J) && player2Chosen == false)
        {
            ChooseGesture(player2Gesture, gestures[0]);
            player2Chosen = true;
        }
        else if (Input.GetKeyDown(KeyCode.K) && player2Chosen == false)
        {
            ChooseGesture(player2Gesture, gestures[1]);
            player2Chosen = true;
        }
        else if (Input.GetKeyDown(KeyCode.L) && player2Chosen == false)
        {
            ChooseGesture(player2Gesture, gestures[2]);
            player2Chosen = true;
        }
    }

    void HandleOnePlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.A) && player1Chosen == false)
        {
            ChooseGesture(player1Gesture, gestures[0]);
            player1Chosen = true;
            ComputerTurn();
        }
        else if (Input.GetKeyDown(KeyCode.S) && player1Chosen == false)
        {
            ChooseGesture(player1Gesture, gestures[1]);
            player1Chosen = true;
            ComputerTurn();
        }
        else if (Input.GetKeyDown(KeyCode.D) && player1Chosen == false)
        {
            ChooseGesture(player1Gesture, gestures[2]);
            player1Chosen = true;
            ComputerTurn();
        }
    }

    void ComputerTurn()
    {
        // Randomly select a gesture for the computer
        int randomIndex = Random.Range(0, gestures.Length);
        ChooseGesture(player2Gesture, gestures[randomIndex]);
        player2Chosen = true;
    }

    void ChooseGesture(GameObject playerGesture, Sprite gestureSprite)
    {
        playerGesture.GetComponent<Image>().sprite = gestureSprite;
        SetAlpha(playerGesture, 1);
    }

    void DetermineWinner()
    {
        Sprite player1Sprite = player1Gesture.GetComponent<Image>().sprite;
        Sprite player2Sprite = player2Gesture.GetComponent<Image>().sprite;

        if (player1Sprite == player2Sprite)
        {
            //tie
        }
        else if ((player1Sprite == gestures[0] && player2Sprite == gestures[2]) ||
                 (player1Sprite == gestures[1] && player2Sprite == gestures[0]) ||
                 (player1Sprite == gestures[2] && player2Sprite == gestures[1]))
        {
            //player 1 wins
            _gameUIHandler.UpdatePlayerOneScore(_player1Score++);
        }
        else
        {
            //player 2 wins
            _gameUIHandler.UpdatePlayerTwoScore(_player2Score++);
        }

        winnerDetermined = true;
    }
}
