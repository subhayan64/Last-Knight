using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{

    public Transform resetPos;
    public Transform targetPos;
    public Transform PlayerPos;
    Vector3 target;
    Vector3 rest;

    bool atTarget;
    bool atRest;

    public float time;
    float timeStore;
    public float attackTime;
    float attackTimeStore;

    public float speed;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;


    public Transform attakPosition;
    public float weaponReach;
    public LayerMask whatIsPlayer;
    public int damage = 15;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    private Animator anim;

    float pos, velocity;
    //bool extremeMode = false;
    //int currentHealth;
    //int maxHealth;
    //public float ExtremeModeWaitTime;

    private AudioManager2 audiomanager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = resetPos.position;
        atTarget = false;
        atRest = true;
        timeStore = time;
        attackTimeStore = attackTime;
        target = targetPos.position;
        rest = resetPos.position;
        anim = GetComponent<Animator>();
        pos = transform.position.x;

        //currentHealth = GetComponent<BossEnemyHealth>().currentHealth;
        //maxHealth = GetComponent<BossEnemyHealth>().maxHealth;

        audiomanager = AudioManager2.instance;
        if (audiomanager == null)
        {
            Debug.LogError("No audio manager in the scene");
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Invoke("growlSound", 3f);

        rest = new Vector3((PlayerPos.position.x + target.x) / 2, resetPos.position.y, 0);
        moveAnimation();

        if (attackTime > 0)
        {
            attackTime -= Time.deltaTime;
        }
        else
        {

            attackTime = attackTimeStore;
            //Do Stuff   
            target = new Vector3(PlayerPos.position.x, targetPos.position.y, 0);

        }


        //if (currentHealth < maxHealth / 2)
        //{
        //    extremeMode = true;
        //}
        //else if (currentHealth < maxHealth / 2 && extremeMode)
        //{
        //    goToReset();
        //    if(atRest)
        //    GetComponent<BossFight>().enabled = false;
        //    if (ExtremeModeWaitTime > 0)
        //    {
        //        ExtremeModeWaitTime -= Time.deltaTime;
        //    }
        //    else
        //    {

        //    }
        //}



        if (atRest)
        {
            
           goToTarget();
            
        }
        else if (atTarget)
        {
            
           goToReset();
            
        }


        if (transform.position == target)
        {
            atTarget = true;
            atRest = false;
        }
        else if (transform.position == resetPos.position)
        {

            //audiomanager.Play("sheep");
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                time = timeStore;
                //Do Stuff   
                atRest = true;
                atTarget = false;
                //contactPlayer = false;
            }
        }

        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }

    }

    void goToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
    }
    void goToReset()
    {
        transform.position = Vector3.MoveTowards(transform.position, resetPos.position, speed * Time.fixedDeltaTime);
    }

    void moveAnimation()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        velocity = (transform.position.x - pos) / Time.fixedDeltaTime;
        pos = transform.position.x;
        //Debug.Log(velocity);

        if (isGrounded == true)
        {
            anim.SetBool("isGrounded", true);
            if (velocity != 0)
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }

        }
        else
        {
            anim.SetBool("isGrounded", false);

        }


    }

    void growlSound()
    {

        if (!audiomanager.isplaying("spiderbossgrowl"))
        {
            audiomanager.Play("spiderbossgrowl");
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

}
