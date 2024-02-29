using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    AudioSource musicSource;

    [SerializeField]
    Button musicButton;

    [SerializeField]
    Sprite[] musicSprites;

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
            musicButton.GetComponent<Image>().sprite = musicSprites[1];
        }
        else
        {
            musicSource.UnPause();
            isMusicPlaying = true;
            // Change button sprite to musicOnSprite
            musicButton.GetComponent<Image>().sprite = musicSprites[0];
        }
    }
}
