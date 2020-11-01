using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fogscript : MonoBehaviour
{

    public Transform pos1;
    public Transform pos2;
    //public GameObject fog;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        //fog.transform.position = new Vector3(pos1.position.x, fog.transform.position.y, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < pos2.position.x)
        {
            transform.position = pos1.position;
            Debug.Log("Yo");
        }
        if(transform.position.x <= pos1.position.x)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

    }
}
