﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    public delegate void TimeIsUpAction();
    public static event TimeIsUpAction TimeIsUp;
    public Animator animator;
    public Camera cam;
    public GameObject Player;
    public float timeToFinish = 90;
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
        if (timeLeft == 0)
        {
            if (TimeIsUp != null) TimeIsUp();
        }
        if (timeLeft == 20)
        {
            AudioManager.instance.Play("StartQuake");
            Player.GetComponent<Player>().rockCreateMultiplier = 1.0f;
            cam.GetComponent<CameraFollow>().isCameraShaking = true;
        }
        if (timeLeft == 10)
        {
            animator.SetBool("isTimeRunningOut", true);
            Player.GetComponent<Player>().rockCreateMultiplier = 0.7f;
            cam.GetComponent<CameraFollow>().isCameraShakingHard = true;
        }
        timer.text = String.Format("{0:00}:{1:00}", timeLeft / 60, timeLeft % 60);
    }
}

