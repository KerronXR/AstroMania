using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGameCanvas : MonoBehaviour
{
    public delegate void NextLevelAction();
    public static event NextLevelAction NextLevel;
    public delegate void MenuLoadAction();
    public static event MenuLoadAction MenuLoad;
    public delegate void PlayAgainAction();
    public static event PlayAgainAction PlayAgain;

    public void NextLevelButton()
    {
        AudioManager.instance.Play("MenuClick");
        Time.timeScale = 1f;
        if (NextLevel != null) NextLevel();
    }

    public void MenuButton()
    {
        AudioManager.instance.Play("MenuClick");
        Time.timeScale = 1f;
        if (MenuLoad != null) MenuLoad();
    }

    public void AgainButton()
    {
        AudioManager.instance.Play("MenuClick");
        Time.timeScale = 1f;
        if (PlayAgain != null) PlayAgain();
    }
}
