using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] enum EnemyType { patrol, follow };
    [SerializeField] EnemyType enemyType;
    //public Transform player;
    public float followRange;
    public LayerMask layerMaskGround;
    public Transform castPoint;


    public Transform attakPosition;
    public float weaponReach;
    public LayerMask whatIsPlayer;
    public int damage = 20;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public float topDistance;
    public float agroDistance;
    public Transform eyeLevel;
    public Transform bodyLevel;



    bool followPlayer = false;
    private float distToPlayer;
    bool moveFlag = true;
    bool wallInBw = false;

    Rigidbody2D rb2d;
    private bool facingRight = false;
    Animator animat;
    bool moveLeft = true;
    public float moveSpeed;
    private bool playerInRange = false;

    //public playerHealth playerHealth;
    //public int burnDamage;

    //public Transform headParticle;
    private AudioManager2 audiomanager;



    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animat = GetComponent<Animator>();

        audiomanager = AudioManager2.instance;
        if (audiomanager == null)
        {
            Debug.LogError("No audio manager in the scene");
        }
    }

    private void Update()
    {
        //distToPlayer = Vector2.Distance(transform.position, player.position);

        if (enemyType == EnemyType.follow)
        {
            SeeRight(followRange);

            if (SeeLeft(followRange))
            {
                followPlayer = true;
            }

            else
            {
                followPlayer = false;
            }
        }

        if (followPlayer)
        {
            if (moveFlag)
            {
                move();
            }
            else
            {
                stopchasing();
            }

        }
        else
        {
            if (moveFlag)
            {
                move();
            }
            else
            {
                stopchasing();
            }
        }

        playerOnTop();
        canSeeLeft(agroDistance);
        checkLedge();

    }
    //to check for player to the enemy's left
    bool SeeLeft(float distance)
    {

        float followDistance;


        if (facingRight)
        {
            followDistance = -distance;
        }
        else
        {
            followDistance = distance;
        }

        Vector2 endPos = bodyLevel.position + Vector3.left * followDistance;

        RaycastHit2D hit = Physics2D.Linecast(bodyLevel.position, endPos, layerMaskGround);

        if (hit.collider != null)
        {
           
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.DrawLine(bodyLevel.position, hit.point, Color.red);
                playerInRange = true;
            }           

            else
            {
                Debug.DrawLine(bodyLevel.position, hit.point, Color.blue);
                playerInRange = false;
            }

        }
        else
        {
            Debug.DrawLine(bodyLevel.position, endPos, Color.cyan);
            playerInRange = false;
        }

        return playerInRange;

    }

    //to check for player to the enemy's right
    bool SeeRight(float distance)
    {

        float followDistance;

        if (facingRight)
        {
            followDistance = -(distance - 4f);
        }
        else
        {
            followDistance = distance - 4f;
        }


        Vector2 endPos = bodyLevel.position + Vector3.right * followDistance;

        RaycastHit2D hit = Physics2D.Linecast(bodyLevel.position, endPos, layerMaskGround);

        if (hit.collider != null)
        {
            Debug.DrawLine(bodyLevel.position, hit.point, Color.black);
            if (hit.collider.gameObject.CompareTag("Player"))
            {

                moveLeft = !moveLeft;
            }

            else
            {
                //Debug.Log(hit.collider.gameObject.name + " behind");
            }

        }
        else
        {
            Debug.DrawLine(bodyLevel.position, endPos, Color.magenta);
            playerInRange = false;
        }

        return playerInRange;
    }

    // to check for player or wall on enemy's left
    void canSeeLeft(float distance)
    {
        float castDistance;
        if (facingRight)
        {
            castDistance = -distance;
        }
        else
        {
            castDistance = distance;
        }

        Vector2 endPos = eyeLevel.position + Vector3.left * castDistance;

        RaycastHit2D hit = Physics2D.Linecast(eyeLevel.position, endPos, layerMaskGround);

        if (hit.collider != null)
        {
            Debug.DrawLine(eyeLevel.position, hit.point, Color.red);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                moveFlag = false;
                Debug.Log("player infront");
                if (Time.time >= nextAttackTime)
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
                else
                {
                    animat.SetBool("isChase", false);
                }
            }
            else if (hit.collider.gameObject.CompareTag("ground"))
            {
                moveFlag = true;
                Debug.Log("ground infront");
                moveLeft = !moveLeft;

            }
            //else if (hit.collider.gameObject.CompareTag("enemy"))
            //{
            //    Debug.DrawLine(bodyLevel.position, hit.point, Color.white);
            //    Debug.Log("another enemy");
            //    moveLeft = !moveLeft;
            //}
            else
            {

                // Debug.Log(hit.collider.gameObject.name + " infront");
            }

        }
        else
        {
            moveFlag = true;

            Debug.DrawLine(eyeLevel.position, endPos, Color.blue);
        }

    }

    //to check for ground in front of enemy
    void checkLedge()
    {
        RaycastHit2D leftLedge = Physics2D.Raycast(new Vector2(castPoint.position.x, castPoint.position.y), Vector2.down, 5f);
        Debug.DrawRay(new Vector2(castPoint.position.x, castPoint.position.y), Vector2.down, Color.red);

        if (leftLedge.collider == null)
        {
            moveLeft = !moveLeft;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            moveLeft = !moveLeft;
            Debug.Log("enemy collision");
        }
        if (collision.gameObject.CompareTag("wall"))
        {
            moveLeft = !moveLeft;
        }
    }

    //to check if player is on top of enenmy
    void playerOnTop()
    {
        Vector2 endPos = transform.position + Vector3.up * topDistance;

        RaycastHit2D hit = Physics2D.Linecast(transform.position, endPos, layerMaskGround);

        if (hit.collider != null)
        {
            Debug.DrawLine(transform.position, hit.point, Color.blue);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                //Debug.Log("player on top");                

                if (Time.time >= nextAttackTime)
                {
                    //hurtByFlame();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
            else
            {
                //Debug.Log("player not on top");
            }
        }
        else
        {
            Debug.DrawLine(transform.position, endPos, Color.magenta);
        }
    }

    //to make the enemy move;
    void move()
    {
        animat.SetBool("isChase", true);
        if (moveLeft == true)
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            //audiomanager.Play("spidermove");
        }
        else if (moveLeft == false)
        {
            rb2d.velocity = new Vector2(moveSpeed, 0);
            //audiomanager.Play("spidermove");
        }
        checkfacing();
    }

    // to make the enemy stop moving
    void stopchasing()
    {
        rb2d.velocity = new Vector2(0, 0);
    }

    //damage caused due to flame
    //void hurtByFlame()
    //{
    //    //DEBUG LATER, when player is disabled, this line throws null reference error
    //    playerHealth.takeDamage(burnDamage);
    //    //Debug.Log("burnt");     

    //}

    //for enemy to attack
    void Attack()
    {
        animat.SetTrigger("attack");

        Collider2D playerToDamage = Physics2D.OverlapCircle(attakPosition.position, weaponReach, whatIsPlayer);


        playerToDamage.GetComponent<playerHealth>().takeDamage(damage);
        Debug.Log("we hit:" + playerToDamage.name);


    }

    //to check the face of enemy according to its movement
    void checkfacing()
    {
        if (facingRight == false && rb2d.velocity.x > 0)
        {
            Flip();
        }
        else if (facingRight == true && rb2d.velocity.x < 0)
        {
            Flip();
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scalar = transform.localScale;
        scalar.x *= -1;
        transform.localScale = scalar;

        //to flip the enemy fire head particles 
        //Vector3 particleScalar = headParticle.localScale;
        //particleScalar.x *= -1;
        //headParticle.localScale = particleScalar;
    }

    //to draw weapon damage reach
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attakPosition.position, weaponReach);
    }
    void playMoveAudio()
    {
        audiomanager.Play("spidermove");
    }

}




//void chaseplayer()
//{
//    animat.SetBool("isChase", true);
//    if (transform.position.x < player.position.x)
//    {
//        rb2d.velocity = new Vector2(moveSpeed, 0);
//    }
//    else
//    {
//        rb2d.velocity = new Vector2(-moveSpeed, 0);
//    }

//    checkfacing();
//}
//void Jump()
//{
//    Debug.Log("Jump");
//}
//private void Jump()
//{
//    isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);


//    if (isGrounded == true)
//    {
//        //anim.SetTrigger("takeoff");
//        rb2d.velocity = Vector2.up * jumpForce;
//    }
//}