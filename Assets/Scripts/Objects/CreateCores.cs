using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCores : MonoBehaviour
{
    public int coreAmount = 50;
    public GameObject core;
    private int randomRollX = 0;
    private int randomRollY = 0;

    void Start()
    {
        for (int i = 1; i <= coreAmount; i++)
        {
            randomRollX = ((int)Mathf.Floor(Random.value * 15)) + 5;
            randomRollY = (int)Mathf.Floor(Random.value * 4);
            Vector2 whereToPutCore = new Vector2(i * randomRollX, -2 + randomRollY);
            Instantiate(core, whereToPutCore, new Quaternion(0, 0, 0, 0));
        }
    }
}
