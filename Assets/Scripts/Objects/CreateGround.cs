using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGround : MonoBehaviour
{
    public int levelSize = 22;
    public GameObject[] ground;
    public GameObject backGround;
    private int randomRoll = 0;

    void Start()
    {
        for (int i = 0; i <= levelSize; i++)
        {
            randomRoll = (int)Mathf.Floor(Random.value * ground.Length);
            Vector2 whereToPutGround = new Vector2(i * 20, -7);
            Instantiate(ground[randomRoll], whereToPutGround, new Quaternion(0, 0, 0, 0));
            Instantiate(backGround, whereToPutGround, new Quaternion(0, 0, 0, 0));
        }
    }

}
