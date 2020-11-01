using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyHealth : MonoBehaviour
{
    public int maxHealth = 300;
    [HideInInspector]
    public int currentHealth;
    //Animator animat;
    //public camerashake shake;
    //Rigidbody2D rb;
    // public GameObject player;

    public GameObject dieeffect;
    public GameObject hurteffect;
    public Transform hurteffectPos;
    public Transform keySpawnPos;
    [HideInInspector]
    public bool BossAlive;
    //public GameObject waterDrop;

    private AudioManager2 audiomanager;

    //public enemyPatrol ep;

    public GameObject key;
    //bool extremeMode = false;
    public float ExtremeModeWaitTime;

    void Start()
    {
        currentHealth = maxHealth;
        //animat = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody2D>();
        audiomanager = AudioManager2.instance;
        if (audiomanager == null)
        {
            Debug.LogError("No audio manager in the scene");
        }
        BossAlive = true;
    }

    public void takeDamage(int damage)
    {
        //play hurt animation
        //animat.SetTrigger("hurt");
        //shake.playShake(0.08f, 0.9f, 5f);


        //play hurt sound
        //audiomanager.Play("hurt2");


        currentHealth -= damage;
        Instantiate(hurteffect, hurteffectPos.position, Quaternion.identity);
        FindObjectOfType<camerashake>().playShake(0.1f, 1.5f, 5f);


        Debug.Log("enemy hurt");
        if (currentHealth <= 0)
        {
            Instantiate(dieeffect, hurteffectPos.position, Quaternion.identity);
            FindObjectOfType<camerashake>().playShake(2f, 3f, 5f);

            //if (ExtremeModeWaitTime > 0)
            //{
            //    ExtremeModeWaitTime -= Time.deltaTime;
            //    Debug.Log(ExtremeModeWaitTime);

            //}
            //else
            //{
            //    Die();
            //}

            Die();


        }
    }
    void Die()
    {
        //die SOund
        audiomanager.Play("spiderdie");

        //shake camera
        FindObjectOfType<camerashake>().playShake(2f, 3f, 5f);


        //die animation

        //animat.SetBool("dead", true);

        audiomanager.StopSound("spiderbossgrowl");

        //Instantiate(dieeffect, hurteffectPos.position, Quaternion.identity);
        //Instantiate(dieeffect, hurteffectPos.position, Quaternion.identity);
        //Instantiate(dieeffect, hurteffectPos.position, Quaternion.identity);
        //Instantiate(waterDrop, transform.position,Quaternion.identity);

        BossAlive = false;
        Instantiate(key, keySpawnPos.position, Quaternion.identity);
        Debug.Log("enemy died!");
        Destroy(gameObject);

    }
}
