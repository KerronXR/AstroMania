using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjects : MonoBehaviour
{
    public GameObject Training;
    private void Start()
    {
        if (GameManager.instance.isTraining == true)
        {
            Instantiate(Training);
        }

    }
}
