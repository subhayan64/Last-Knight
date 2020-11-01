using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;
    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;


    private Rigidbody2D rb;

    private Animator anim;


    public camerashake shake;


    public Transform attakPosition;
    public float attackRange;
    public LayerMask whatIsEnemy;
    int damage = 30;
    public LayerMask whatIsbossEnemy;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    private AudioManager2 audiomanager;

    //public Transform endframePos;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        audiomanager = AudioManager2.instance;
        if (audiomanager == null)
        {
            Debug.LogError("No audio manager in the scene");
        }
    }
    private void FixedUpdate()
    {
        move();

        



        if (Input.GetKeyDown("left") || Input.GetKeyDown("right"))
        {
            audiomanager.Play("run");
        }
        else if (Input.GetKeyUp("left") || Input.GetKeyUp("right"))
        {
            audiomanager.StopSound("run");
        }
        else if (Input.GetKeyDown("up"))
            audiomanager.Play("takeOff");


        if (isGrounded)
        {
            // audiomanager.StopSound("run");
            if (Input.GetKey("left") || Input.GetKey("right"))
            {
                if (!audiomanager.isplaying("run"))
                {
                    audiomanager.Play("run");
                }
            }
            else
            {
                audiomanager.StopSound("run");
            }

        }
        else if (!isGrounded)
        {
            audiomanager.StopSound("run");
        }

    }
    private void Update()
    {
        jump();

        if (Time.time >= nextAttackTime)
        {
            //then attack;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {


        anim.SetTrigger("attack");

        //play attack sound
        audiomanager.Play("attack");


        rb.AddForce(new Vector2(facingRight?200:-200, 0), ForceMode2D.Force);
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attakPosition.position, attackRange, whatIsEnemy);

        //Collider2D[] spider3ToDamage = Physics2D.OverlapCircleAll(attakPosition.position, attackRange, whatIsEnemy);


        foreach (Collider2D enemy in enemiesToDamage)
        {
            enemy.GetComponent<enemyHealth>().takeDamage(damage);
            //enemy.GetComponent<spider3Health>().takeDamage(damage);
            //Debug.Log("we hit:" + enemy.name);
        }

        Collider2D BossSpiderToDamage = Physics2D.OverlapCircle(attakPosition.position, attackRange, whatIsbossEnemy);
        BossSpiderToDamage.GetComponent<BossEnemyHealth>().takeDamage(damage);


        //foreach (Collider2D enemy in spider3ToDamage)
        //{
        //    enemy.GetComponent<spider3Health>().takeDamage(damage);
        //    //Debug.Log("we hit:" + enemy.name);
        //}


    }
    private void move()
    {
        //play run sound


        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);




        if (moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }
    private void jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isGrounded == true)
        {
            anim.SetBool("isJumping", false);

        }
        else
        {
            anim.SetBool("isJumping", true);

        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == true)
        {
            //play take off sound


            anim.SetTrigger("takeoff");
            rb.velocity = Vector2.up * jumpForce;
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        /*Vector3 scalar = transform.localScale;
         scalar.x *= -1;
         transform.localScale = scalar;  */
        transform.Rotate(0f, 180f, 0f);
    }
    void PlayCameraShake()
    {
        shake.playShake(0.08f, 0.8f, 5f);
    }

    void OnDrawGizmosSelected()
    {
        if (attakPosition == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attakPosition.position, attackRange);
    }

    public void landSound()
    {
        audiomanager.Play("land");
    }

    //public void takeOffSound()
    //{
    //    audiomanager.Play("takeOff");
    //}


}
