using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    private float Speed = 0.1f;
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * Speed);
        Invoke("SpeedUp", 1.0f);
    }
    private void SpeedUp()
    {
        if (Speed < 20)
        Speed += 0.05f;
    }
}
