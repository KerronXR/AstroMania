using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training : MonoBehaviour
{
    private GameObject CountDown;
    private GameObject Player;
    private GameObject UICanvas;
    private float timeToFinish;
    private float runSpeed;
    void Start()
    {
        CountDown = GameObject.Find("CountDown");
        timeToFinish = CountDown.GetComponent<Timer>().timeToFinish;
        CountDown.GetComponent<Timer>().timeToFinish = 3599;
        Player = GameObject.Find("Player");
        runSpeed = Player.GetComponent<Player>().runSpeed;
        Player.GetComponent<Player>().runSpeed = 0;
        Player.GetComponent<Player>().isTraining = true;
        UICanvas = GameObject.Find("UI Canvas");
        UICanvas.SetActive(false);
        Invoke("Release", 15f);
    }

    // Update is called once per frame
    void Release()
    {
        UICanvas.SetActive(true);
        Player.GetComponent<Player>().runSpeed = runSpeed;
        Player.GetComponent<Player>().isTraining = false;
        CountDown.GetComponent<Timer>().timeToFinish = timeToFinish + 16;

    }
}
