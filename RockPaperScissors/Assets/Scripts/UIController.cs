using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIController : MonoBehaviour
{

    //Quit
    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; //to close in the unity editor
#endif
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
