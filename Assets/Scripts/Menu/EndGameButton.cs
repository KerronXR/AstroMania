using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGameButton : MonoBehaviour
{
    public void AgainButton()
    {
        SceneManager.LoadScene("Lv1");
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
