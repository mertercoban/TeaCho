using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Audio source for background music
    [SerializeField] AudioSource musicSource;

    // Audio source for sound effects
    [SerializeField] AudioSource musicSfx;

    // Audio clip for background music
    public AudioClip background;

    // Audio clip for walking sound effect
    public AudioClip walk;

    // Audio clip for teleport sound effect
    public AudioClip teleport;

    // Called at the start of the game
    void Start() {
        // Set the background music clip to the audio source
        musicSource.clip = background;

        // Play the background music
        musicSource.Play();
    }

    // Method to play a sound effect
    public void PlaySFX(AudioClip audio) {
        // If a sound effect is already playing, stop it
        if (musicSfx.isPlaying) {
            musicSfx.Stop(); 
        }
        
        // Play the specified sound effect once
        musicSfx.PlayOneShot(audio);  
    }
}
