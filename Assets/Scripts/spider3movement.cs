using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider3movement : MonoBehaviour
{

    public Transform resetPos;
    public Transform targetPos;
    
   

    bool atTarget;
    bool atRest;

    public float timeOsc;
  
    public float speed;

    public Transform attakPosition;
    public float weaponReach;
    public LayerMask whatIsPlayer;
    public int damage = 15;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    private Animator anim;

    Vector3 rest;
    Vector3 target;

    private AudioManager2 audiomanager;


    // Start is called before the first frame update
    void Start()
    {
        //transform.position = resetPos.position;
        audiomanager = AudioManager2.instance;
        if (audiomanager == null)
        {
            Debug.LogError("No audio manager in the scene");
        }

        anim = GetComponent<Animator>();
        rest = new Vector3(transform.position.x, resetPos.position.y, 0);
        target = new Vector3(transform.position.x, targetPos.position.y, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        
        transform.position = Vector3.Lerp(rest, target, Mathf.PingPong(Time.time * speed, timeOsc));

        

        //Attack();
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }

    }  


    void Attack()
    {
        //animat.SetTrigger("attack");
        Collider2D playerToDamage = Physics2D.OverlapCircle(attakPosition.position, weaponReach, whatIsPlayer);
        playerToDamage.GetComponent<playerHealth>().takeDamage(damage);
        Debug.Log("we hit:" + playerToDamage.name);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attakPosition.position, weaponReach);
    }

    void playMoveAudio()
    {
        if (!audiomanager.isplaying("spider3Sound"))
        {
            audiomanager.Play("spider3Sound");
        }
    }


}
