using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int lastPlayedSceneIndex;
    private float SoundVolume, MusicVolume;
    private int qualityIndex;
    public AudioMixer audioMixer;
    public bool isTraining = false;

    private void Awake()
    {
        MakeSingleton();
    }

    private void Start()
    {
        SoundVolume = PlayerPrefs.GetFloat("SoundVolume");
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
        qualityIndex = PlayerPrefs.GetInt("qualityIndex");
        lastPlayedSceneIndex = PlayerPrefs.GetInt("lastPlayedSceneIndex");
        if (lastPlayedSceneIndex == 0) lastPlayedSceneIndex = 1;
        audioMixer.SetFloat("MusicVolume", MusicVolume);
        audioMixer.SetFloat("SoundVolume", SoundVolume);
        QualitySettings.SetQualityLevel(qualityIndex);
        Player.DiedOnLava += LoadGameOverScene;  // register player die on lava event
        Player.WinLevel += LoadWinScene;  // register player win level event
        Timer.TimeIsUp += LoadGameOverScene;  // register time is up event
        EndGameCanvas.NextLevel += LoadNextScene; // register next level event
        EndGameCanvas.MenuLoad += LoadMenuScene; // register menu load event
        EndGameCanvas.PlayAgain += LoadSameScene; // register play again event
        MainMenu.PlayGame += LoadSameScene; // register menu continue level event
        MainMenu.PlayLevel += LoadLevel;  // register menu load level event
        PauseMenu.MenuLoad += LoadMenuScene; // register menu load event for pause menu
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
            DontDestroyOnLoad(gameObject);
        }

    }

    void LoadNextScene()
    {
        Invoke("StopAllSounds", 0.1f);
        AudioManager.instance.StopTracks();
        SceneManager.LoadScene("Lv" + (lastPlayedSceneIndex + 1));
        Scene Try = SceneManager.GetSceneByName("Lv" + (lastPlayedSceneIndex + 1));
        if (!Try.IsValid()) // next level not exists
        {
            Debug.Log("No next level - go to 1");
            lastPlayedSceneIndex = 1;
            PlayerPrefs.SetInt("lastPlayedSceneIndex", lastPlayedSceneIndex);
            SceneManager.LoadScene("Lv1");
            AudioManager.instance.PlayTrack("Track1");
        }
        else
        {
            lastPlayedSceneIndex++;
            PlayerPrefs.SetInt("lastPlayedSceneIndex", lastPlayedSceneIndex);
            AudioManager.instance.PlayTrack("Track" + lastPlayedSceneIndex);
        }

    }

    void LoadGameOverScene()
    {
        AudioManager.instance.StopSounds();
        AudioManager.instance.StopTracks();
        Scene currScene = SceneManager.GetActiveScene();
        lastPlayedSceneIndex = currScene.buildIndex;  // store the level played before the lose screen
        PlayerPrefs.SetInt("lastPlayedSceneIndex", lastPlayedSceneIndex);
        if (UnityEngine.StackTraceUtility.ExtractStackTrace().Contains("checkTemperature"))
        {
            // Die on lava is level feature
            // So no load lose scene just register last played level
            AudioManager.instance.Play("Wasted");
        }
        else
        {
            SceneManager.LoadScene("GameOver");
            AudioManager.instance.Play("StarBlast");
        }
    }

    void LoadWinScene()
    {
        PlayerPrefs.SetInt("isTraining", 0);
        isTraining = false;
        Scene currScene = SceneManager.GetActiveScene();
        lastPlayedSceneIndex = currScene.buildIndex;  // store the level played before the win screen
        PlayerPrefs.SetInt("lastPlayedSceneIndex", lastPlayedSceneIndex);
        String currSceneNumber = currScene.name.Substring(2, 1);
        SceneManager.LoadScene("Win" + currSceneNumber);
        AudioManager.instance.StopSounds();
        AudioManager.instance.StopTracks();
        AudioManager.instance.PlayTrack("Win");
        AudioManager.instance.Play("Spaceship");
    }

    void LoadSameScene()
    {
        if (PlayerPrefs.GetInt("isTraining") == 1)
        {
            isTraining = true;
            LoadLevel("Lv1");
        }
        else
        {
            isTraining = false;
            Invoke("StopAllSounds", 0.1f);
            AudioManager.instance.StopTracks();
            SceneManager.LoadScene("Lv" + lastPlayedSceneIndex);
            AudioManager.instance.PlayTrack("Track" + lastPlayedSceneIndex);
        }
    }

    void LoadMenuScene()
    {
        Invoke("StopAllSounds", 0.1f);
        AudioManager.instance.StopTracks();
        SceneManager.LoadScene("MainMenu");
    }

    void LoadLevel(string name)
    {
        Invoke("StopAllSounds", 0.1f);
        AudioManager.instance.StopTracks();
        SceneManager.LoadScene(name);
        int index = int.Parse(name.Substring(2, 1));
        lastPlayedSceneIndex = index;
        PlayerPrefs.SetInt("lastPlayedSceneIndex", lastPlayedSceneIndex);
        AudioManager.instance.PlayTrack("Track" + name.Substring(2, 1));
    }

    void StopAllSounds() // make room for click sound before load
    {
        AudioManager.instance.StopSounds();
    }
}
