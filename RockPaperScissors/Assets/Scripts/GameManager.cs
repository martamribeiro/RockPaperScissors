using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public Sprite[] gestures;

    public TMP_Text player1, player2;

    public void StartOnePlayerGame()
    {
        //changes in one player game
        player2.text = "Computer";
        StartGame();
    }

    public void StartTwoPlayersGame()
    {
        //changes in two players game
        player2.text = "Player 2";
        StartGame();
    }

    public void StartGame()
    {
        //common to 1 player and 2 players game modes
    }

    

}
