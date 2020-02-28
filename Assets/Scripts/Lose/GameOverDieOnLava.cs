using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverDieOnLava : MonoBehaviour
{
    public GameObject UICanvas;
    public GameObject Panel;
    public GameObject Buttons;

    public void LoadGameOverDieOnLavaCanvas()
    {
        UICanvas.SetActive(false);
        Invoke("LoadPanel", 1.2f);
        Invoke("LoadButtons", 2.1f);
    }

    void LoadPanel()
    {
        Panel.SetActive(true);
    }

    void LoadButtons()
    {
        Buttons.SetActive(true);
    }

}
