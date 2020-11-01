using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameSwitch_position : MonoBehaviour
{
    public GameObject Frame1;
    public GameObject Frame2;
    public Transform position1;
    public Transform position2;
    public GameObject PlayerPosition;

    
    public void OnTriggerExit2D(Collider2D collision)
    {
        Vector3 pos1 = position1.position;
        Vector3 pos2 = position2.position;
        if (collision.tag == "Player")
        {
            
            if (Frame1.active == true)
            {
                Frame1.SetActive(false);
                Frame2.SetActive(true);
                PlayerPosition.transform.position = pos1;

            }
            else if (Frame1.active == false)
            {
                Frame1.SetActive(true);
                Frame2.SetActive(false);
                PlayerPosition.transform.position = pos2;
            }
        }

    }
}
