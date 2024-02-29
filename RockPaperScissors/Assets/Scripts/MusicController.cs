using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource musicSource;
    public GameObject musicStopped;

    private bool isMusicPlaying = true;

    private void Start()
    {
        // Ensure music is initially playing
        musicSource.Play();
    }

    public void ToggleMusic()
    {
        if (isMusicPlaying)
        {
            musicSource.Pause();
            isMusicPlaying = false;
            // Change button sprite to musicOffSprite
            musicStopped.SetActive(true);
        }
        else
        {
            musicSource.UnPause();
            isMusicPlaying = true;
            // Change button sprite to musicOnSprite
            musicStopped.SetActive(false);
        }
    }
}
