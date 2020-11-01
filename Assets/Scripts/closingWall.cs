using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closingWall : MonoBehaviour
{

    public Transform closingRestRight;
    public Transform closingTargetRight;
    public Transform wallRight;

    public Transform closingRestLeft;
    public Transform closingTargetLeft;
    public Transform wallLeft;

    public float speed;

    public BossEnemyHealth Health;

    bool playerInRange;
    private Rigidbody2D rb;
    bool shakenOnce = false;
    private AudioManager2 audiomanager;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("player entered");
            
            playerInRange = true;
        }
    }

    public void Start()
    {
        playerInRange=  false;
        rb = GetComponent<Rigidbody2D>();

        audiomanager = AudioManager2.instance;
        if (audiomanager == null)
        {
            Debug.LogError("No audio manager in the scene");
        }
    }
    public void Update()
    {
        if (Health.BossAlive == false)
        {
            playerInRange = false;
            
            Invoke("moveToRest", 2);

        }
        if (playerInRange)
        {
            moveToTarget();
        }
        Debug.Log(Health.BossAlive);

    }

    void moveToTarget()
    {
        if (!shakenOnce)
        {
            FindObjectOfType<camerashake>().playShake(1f, 3f, 5f);
            shakenOnce = true;
            audiomanager.Play("closingwall");

        }

        
        wallRight.position = Vector3.MoveTowards(wallRight.position, closingTargetRight.position, speed * Time.fixedDeltaTime);
        wallLeft.position = Vector3.MoveTowards(wallLeft.position, closingTargetLeft.position, speed * Time.fixedDeltaTime);
        Destroy(rb);
        Destroy(GetComponent<BoxCollider2D>());

    }
    void moveToRest()
    {
        if (!shakenOnce)
        {
            FindObjectOfType<camerashake>().playShake(1f, 3f, 5f);
            shakenOnce = true;
            audiomanager.Play("closingwall");
        }       

        wallRight.position = Vector3.MoveTowards(wallRight.position, closingRestRight.position, speed * Time.fixedDeltaTime);
        wallLeft.position = Vector3.MoveTowards(wallLeft.position, closingRestLeft.position, speed * Time.fixedDeltaTime);

        //Debug.Log("move to rest");
    }
}
