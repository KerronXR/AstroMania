using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static float timeToFinish = 45;
    private int timeLeft;
    Text timer;

    void Start()
    {
        timer = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        timeLeft = ((int)(timeToFinish - Time.timeSinceLevelLoad));
        if (timeLeft <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            timer.text = "Time Left: " + timeLeft;
        }
    }
}

