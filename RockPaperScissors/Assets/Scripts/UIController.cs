using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIController : MonoBehaviour
{

    //Quit
    public void Quit()
    {
        EditorApplication.isPlaying = false; //to close in the unity editor
        Application.Quit();
    }
}
