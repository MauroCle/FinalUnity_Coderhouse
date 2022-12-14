using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControler : MonoBehaviour
{
    public Slider PauseVolumeSlide;
    public Slider LoseVolumeSlide;
    public static bool pitchReduced = false;
    AudioSource audioSource;
    public List<AudioSource> FSXAudioSources = new List<AudioSource>();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = GameManager.MusicVolume / 100;

        foreach (var item in FSXAudioSources)
        {
            item.volume = GameManager.MusicVolume / 100;
        }

        ShipColisionDetector.Collided += ReducePitch;
        UpdatePauseSlider();
        UpdateLoseSlider();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (pitchReduced && audioSource.pitch > 0)
            audioSource.pitch -= 0.02f;
        else if (!pitchReduced && audioSource.pitch < 1)
            audioSource.pitch += 0.03f;
    }

    public void OnPauseVolumeChange()
    {
        GameManager.MusicVolume = PauseVolumeSlide.value;
        audioSource.volume = GameManager.MusicVolume / 100;
        foreach (var item in FSXAudioSources)
        {
            item.volume = GameManager.MusicVolume / 100;
        }
        UpdateLoseSlider();
    }

    public void OnLoseVolumeChange()
    {
        GameManager.MusicVolume = LoseVolumeSlide.value;
        audioSource.volume = GameManager.MusicVolume / 100;
        foreach (var item in FSXAudioSources)
        {
            item.volume = GameManager.MusicVolume / 100;
        }
        UpdatePauseSlider();
    }

    void UpdatePauseSlider()
    {
        PauseVolumeSlide.value = GameManager.MusicVolume;
    }

    void UpdateLoseSlider()
    {
        LoseVolumeSlide.value = GameManager.MusicVolume;
    }

    void ReducePitch()
    {
        pitchReduced = true;
    }
    private void OnDisable()
    {
        ShipColisionDetector.Collided -= ReducePitch;
    }

}
