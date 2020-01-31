using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public delegate void PlaylevelAction(string name);
    public static event PlaylevelAction PlayLevel;
    public delegate void PlayGameAction();
    public static event PlayGameAction PlayGame;
    public GameObject soundAudioSlider;
    public GameObject musicAudioSlider;
    public GameObject dropDown;
    public AudioMixer audioMixer;
    private bool soundCooldown = true;

    public void Start()
    {
        AudioManager.instance.PlayTrack("MainMenu");
        float SoundVolume = PlayerPrefs.GetFloat("SoundVolume");
        float MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
        int qualityIndex = PlayerPrefs.GetInt("qualityIndex");
        soundAudioSlider.GetComponent<Slider>().value = SoundVolume;
        musicAudioSlider.GetComponent<Slider>().value = MusicVolume;
        dropDown.GetComponent<TMP_Dropdown>().value = qualityIndex;
        RemoveTestSoundCooldown();
    }
    public void GoPlayGame()
    {
        if (PlayGame != null) PlayGame();
    }

    public void GoPlayLevel(string name)
    {
        if (PlayLevel != null) PlayLevel(name);
    }

    public void PlayClick(string name)
    {
        AudioManager.instance.Play(name);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void SetSoundVolume(float volume)
    {
        if (soundCooldown == false) PlayTestSound();
        audioMixer.SetFloat("SoundVolume", volume);
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }

    void PlayTestSound()
    {
        AudioManager.instance.Play("Boom1");
        soundCooldown = true;
        Invoke("RemoveTestSoundCooldown", 0.4f);
    }

    void RemoveTestSoundCooldown()
    {
        soundCooldown = false;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("qualityIndex", qualityIndex);
    }
}
