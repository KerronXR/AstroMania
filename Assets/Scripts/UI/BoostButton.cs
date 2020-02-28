using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BoostButton : MonoBehaviour
{
    public Sprite[] buttonImages;
    public Animator animator;
    public GameObject TapMeCanvas;
    private void Start()
    {
        if (GameManager.instance.isTraining == false)
        {
            Destroy(TapMeCanvas);
        }
        else
        {
            TapMeCanvas.SetActive(false);
        }
    }
    public void SetBoostImage(int frameNo)
    {
        transform.GetComponent<Image>().sprite = buttonImages[frameNo];
    }

    public void AnimateReady(bool state)
    {
        if (state == true)
        {
            animator.SetBool("isReady", true);
            if (TapMeCanvas != null) TapMeCanvas.SetActive(true);
        }
        else
        {
            animator.SetBool("isReady", false);
            if (TapMeCanvas != null) TapMeCanvas.SetActive(false);
        }
    }
}
