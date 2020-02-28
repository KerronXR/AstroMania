using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTimesUp : MonoBehaviour
{
    public GameObject MenuCanvas;
    void Start()
    {
        Invoke("LoadMenuCanvas", 4f);
    }

    void LoadMenuCanvas()
    {
        MenuCanvas.SetActive(true);
    }
}
