using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Sprite[] gestures; //order: rock, paper, scissors

    public TMP_Text player1, player2, player1Points, player2Points;

    public GameObject player1Gesture, player2Gesture;

    bool isOnePlayerMode = false;
    bool player1Chosen = false;
    bool player2Chosen = false;
    bool winnerDetermined = false;

    void Start()
    {
        SetAlpha(player1Gesture, 0);
        SetAlpha(player2Gesture, 0);
    }

    void SetAlpha(GameObject emptySprite, float alpha)
    {
        var image = emptySprite.GetComponent<Image>();
        var color = image.color;
        color.a = alpha;
        image.color = color;
    }

    public void StartOnePlayerGame()
    {
        isOnePlayerMode = true;
        StartGame();
    }

    public void StartTwoPlayersGame()
    {
        isOnePlayerMode = false;
        StartGame();
    }

    public void StartGame()
    {
        SetAlpha(player1Gesture, 0);
        SetAlpha(player2Gesture, 0);
        player1Points.text = "0";
        player2Points.text = "0";
        player1Chosen = false;
        player2Chosen = false;
        if (!isOnePlayerMode)
        {
            player1.text = "Player 1";
            player2.text = "Player 2";
        }
        else
        {
            player1.text = "Player 1";
            player2.text = "Computer";
        }
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

    void Update()
    {
        if (player1Chosen && player2Chosen && winnerDetermined == false)
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

    void HandleTwoPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.A)&&player1Chosen==false)
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
            UpdatePoints(player1Points);
        }
        else
        {
            //player 2 wins
            UpdatePoints(player2Points);
        }
        winnerDetermined = true;
    }

    void UpdatePoints(TMP_Text playerPoints)
    {
        int points = int.Parse(playerPoints.text);
        points++;
        playerPoints.text = points.ToString();
    }
}
