using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTarget : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            /*
            RaycastHit2D hit;
            Ray2D ray = Camera.main.ScreenPointToRay(Input.mousePosition.x, Input.mousePosition.y);
            //Debug.Log("In click");
            if (Physics2D.Raycast(ray, out hit, 500.0f))
            {
                Debug.Log("In Phycis raysact");
                if (hit.transform != null)
                {

                    Rigidbody rb;

                    if (rb = hit.transform.GetComponent<Rigidbody>())
                    {
                        print(hit.transform.gameObject);
                    }
                }
            }
            */
        }
    }

    private void PrintName(GameObject go)
    {
        print(go.name);
    }

}
