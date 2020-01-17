using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver1 : MonoBehaviour
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
