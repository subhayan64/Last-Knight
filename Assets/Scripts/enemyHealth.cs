using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    Animator animat;
    //public camerashake shake;
    Rigidbody2D rb;
   // public GameObject player;

    public GameObject dieeffect;
    public GameObject hurteffect;
    //public GameObject waterDrop;
    public Transform hurtPos;

    private AudioManager2 audiomanager;


    //public enemyPatrol ep;

    void Start()
    {
        currentHealth = maxHealth;
        animat = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audiomanager = AudioManager2.instance;
        if (audiomanager == null)
        {
            Debug.LogError("No audio manager in the scene");
        }
    }

   public void takeDamage(int damage)
    {
        //play hurt animation
        //animat.SetTrigger("hurt");
        //shake.playShake(0.08f, 0.9f, 5f);


        //play hurt sound
        //audiomanager.Play("hurt2");


        currentHealth -= damage;
        audiomanager.Play("spiderhurt");
        Instantiate(hurteffect, hurtPos.position, Quaternion.identity);
        FindObjectOfType<camerashake>().playShake(0.1f, 1.5f, 5f);


        Debug.Log("enemy hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
       

    }
    void Die()
    {
        //die SOund
       audiomanager.Play("spiderdie");

        //shake camera
        FindObjectOfType<camerashake>().playShake(0.5f, 3f, 5f);
    

    //die animation

    //animat.SetBool("dead", true);
    Instantiate(dieeffect, hurtPos.position, Quaternion.identity);
        //Instantiate(waterDrop, transform.position,Quaternion.identity);
        Debug.Log("enemy died!");
        Destroy(gameObject);
        
    }
}
