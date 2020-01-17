﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Thermometer : MonoBehaviour
{
    public Sprite[] thermometerImages;
    public void SetThermo(int frameNo)
    {
        transform.GetComponent<SpriteRenderer>().sprite = thermometerImages[frameNo];
    }
}
