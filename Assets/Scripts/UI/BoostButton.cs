using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BoostButton : MonoBehaviour
{
    public Sprite[] buttonImages;
    public Animator animator;

    public void SetBoostImage(int frameNo)
    {
        transform.GetComponent<Image>().sprite = buttonImages[frameNo];
    }
}
