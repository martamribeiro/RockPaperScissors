using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public static void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; //to close in the unity editor
#endif
        Application.Quit();
    }

    public static void ChangeToGameScene()
    {
        SceneManager.LoadScene("GameScene 1");
    }

    public static void ChangeToSinglePlayerGameScene()
    {
        GameManager.Instance.StartSinglePlayerGame();
        ChangeToGameScene();
    }

    public static void ChangeToMultiPlayerGameScene()
    {
        GameManager.Instance.StartMultiPlayerGame();
        ChangeToGameScene();
    }

    public static void GoBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
