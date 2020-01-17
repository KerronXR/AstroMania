using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRockWalls : MonoBehaviour
{
    public int levelSize = 22;
    public GameObject[] RockTypes;
    private int randomRoll = 0;

    void Start()
    {
        for (int i = 1; i <= levelSize; i++)
        {
            randomRoll = (int)Mathf.Floor(Random.value * RockTypes.Length); // choose the type
            Vector2 whereToPut = new Vector2(i * 30 + (Random.value * 20), 0);
            Instantiate(RockTypes[randomRoll], whereToPut, new Quaternion(0, 0, 0, 0));
        }
    }

}
