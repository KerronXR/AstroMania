using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string currentlevel;
    public float volume;
    public int graphicSettingsLevel;
    public AudioMixer audionMixer;

    public void SetVolume()
    {
        audionMixer.SetFloat("volume", volume);
    }

    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(graphicSettingsLevel);
    }

    private void Awake()
    {
        MakeSingleton();
    }

    void MakeSingleton()
    {

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad (gameObject);
        }

    }
}
