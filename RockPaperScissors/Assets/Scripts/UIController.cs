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

    //Show something
    public void showIt(GameObject obj)
    {
        obj.SetActive(true);
    }

    //Hide something
    public void hideIt(GameObject obj)
    {
        obj.SetActive(false);
    }
}
