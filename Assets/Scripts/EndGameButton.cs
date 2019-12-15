using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGameButton : MonoBehaviour
{
    public void PressButton()
    {
        SceneManager.LoadScene("Lv1");
    }
}
