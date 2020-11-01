using UnityEngine;

public class VirusMove : MonoBehaviour
{

    //public float speed = 0.5f;
    //public float frequency = 5.0f; // Speed of sine movement
    //public float magnitude = 0.11f; //  Size of sine movement

    //public float time = 2f;
    //float timeStore;
    //int ran;
    //Vector3 pos;
    //Vector3 axis;
    //SpriteRenderer sr;



    public float angle, radius = 10;
    public float angleSpeed = 2;
    public float radialSpeed = 0.5f;
 
    private void Awake()
    {
        //pos = transform.position;
        //axis = transform.up;
        //sr = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        //timeStore = time;
    }

    void FixedUpdate()
    {
        //ran = Random.Range(0, 2);
        //if (time > 0)
        //{
        //    time -= Time.deltaTime;
        //}
        //else
        //{
        //    time = timeStore;
        //    //Do Stuff          

        //    if (ran == 0)
        //    {
        //        pos += Vector3.right * Time.deltaTime * speed;
        //    }
        //    if (ran == 1)
        //    {
        //        pos += Vector3.left * Time.deltaTime * speed;
        //    }            
        //}



        //Vector3 temp = new Vector3(Random.Range(-10.6f, 10.6f), Random.Range(-10.6f, 10.6f), Random.Range(-10.6f, 10.6f));
        //transform.position = temp;
        //transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;

        angle += Time.deltaTime * angleSpeed;
        radius -= Time.deltaTime * radialSpeed;

        float x = radius * Mathf.Cos(Mathf.Deg2Rad * angle);
        float y = radius * Mathf.Sin(Mathf.Deg2Rad * angle);
        float z = transform.position.y;

        transform.position = new Vector3(x, y, z);

    }
}