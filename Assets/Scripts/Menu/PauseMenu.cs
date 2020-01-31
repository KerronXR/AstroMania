using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public delegate void MenuLoadAction();
    public static event MenuLoadAction MenuLoad;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public void PauseResumeGame()
    {
        if (!GameIsPaused)
        {
            AudioManager.instance.PauseAll();
            AudioManager.instance.Play("MenuPause");
            pauseMenuUI.SetActive(true);
            GameIsPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            AudioManager.instance.Play("MenuResume");
            AudioManager.instance.ResumeAll();
            pauseMenuUI.SetActive(false);
            GameIsPaused = false;
            Time.timeScale = 1f;
        }
    }

    public void QuitGame()
    {
        AudioManager.instance.Play("MenuClick");
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        AudioManager.instance.Play("MenuClick");
        Time.timeScale = 1f;
        if (MenuLoad != null) MenuLoad();
    }
}
