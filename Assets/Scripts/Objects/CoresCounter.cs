using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CoresCounter : MonoBehaviour
{
    public static float timeToFinish = 90;
    private int coresAmount;
   Text coreCounter;

    void Start()
    {
        coreCounter = GetComponent<Text>();
    }
    // Update is called once per frame
    public void SetCoresAmount(int amount)
    {
       coreCounter.text = String.Format("Cores: {0:00}", amount);
    }
}

