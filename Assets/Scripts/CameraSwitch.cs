using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    public GameObject cameraOne;
    public GameObject cameraTwo;

    AudioListener cameraOneAudioLis;
    AudioListener cameraTwoAudioLis;

    void Start()
    {
        cameraOneAudioLis = cameraOne.GetComponent<AudioListener>();
        cameraTwoAudioLis = cameraTwo.GetComponent<AudioListener>();
        cameraChange(false);
    }
    public void cameraChange(bool camSwitch)
    {
        cameraOne.gameObject.SetActive(!camSwitch);
        cameraOneAudioLis.enabled = !camSwitch;
        cameraTwo.gameObject.SetActive(camSwitch);
        cameraTwoAudioLis.enabled = camSwitch;
    }
}