using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameSwitch : MonoBehaviour
{
    public GameObject Frame1;
    public GameObject Frame2;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Frame1.active == true)
            {
                Frame1.SetActive(false);
                Frame2.SetActive(true);
            }
            else if (Frame1.active == false)
            {
                Frame1.SetActive(true);
                Frame2.SetActive(false);
            }
        }

    }
}
