using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLava : MonoBehaviour
{
    public GameObject lava1;
    public GameObject lava2;
    void Start()
    {
        for (int i = -1; i <= 1; i++)
        {
            Vector2 whereToPutLava = new Vector2(i * 20, -7);
            Instantiate(lava1, whereToPutLava, new Quaternion(0, 0, 0, 0));
            Instantiate(lava2, whereToPutLava, new Quaternion(0, 0, 0, 0));
        }
    }
}
