using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadScreenPlayer : MonoBehaviour
{
    public float Speed = 10f;
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * Speed);
    }
}
