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
        SceneManager.LoadScene("GameScene");
    }

    public static void ChangeToSinglePlayerGameScene()
    {
        ChangeToGameScene();
        GameManager.Instance.StartSinglePlayerGame();
    }

    public static void ChangeToMultiPlayerGameScene()
    {
        ChangeToGameScene();
        GameManager.Instance.StartMultiPlayerGame();
    }

    public static void GoBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
