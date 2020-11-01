using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    [HideInInspector]
    public int currentHealth;
    public int powerUp;
   // Animator animat;
    public camerashake shake;
   
    //public GameObject player;

    public GameObject effect;
    public GameObject hurteffect;
    public Transform diepos;
    //public enemyPatrol ep;

    public healthBar healthBar;

    public GameObject Filter;

    public Animator filterAnim;

    public Image key;

    public bool collectedKey;

    public Transform cageDoor;
    public float cageDoorSpeed = 3;
    public Transform CageRest;
    public Transform CageTarget;

    bool cageMove;

    //public Transform startframepos;

    //public int numOfHearts;

    //[HideInInspector]
    //public int heartsLeft;

    //public Image[] hearts;

    ////public gameOver gameOver;

    private AudioManager2 audiomanager;
    //Transform playerPos;

    ////public score score;

    //public TextMeshProUGUI respawningTime;

    //public GameObject pausebutton;

    public Transform endframePos;


    private GameManager _manager;
    private int _lives;
    public TextMeshProUGUI lifeLeft;
    public GameObject gameoverCanvas;
    public GameObject pauseButton;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("key"))
        {

            key.enabled = true;
            Destroy(collision.gameObject);
            collectedKey = true;

        }

        if (collision.gameObject.CompareTag("cage"))
        {
            if (collectedKey)
            {
                Debug.Log("cage move");
                cageMove = true;
                key.enabled = false;
            }
        }
        //healthBar.setHealthValue(currentHealth);

            //if (collision.gameObject.CompareTag("ClosingWall"))
            //{
            //    Debug.Log("player entered");
            //    //FindObjectOfType<camerashake>().playShake(2f, 3f, 5f);
            //    //transform.position = Vector3.MoveTowards(transform.position, closingTarget.position, speed * Time.fixedDeltaTime);
            //}
    }

    
    private void Awake()
    {
        //The following line should work if you stick to having one GameManager in the game
        _manager = GameObject.FindObjectOfType<GameManager>();
        _lives = _manager.livesLeft;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxHealth(maxHealth);
        //animat = GetComponent<Animator>();
        //heartsLeft = numOfHearts;

        audiomanager = AudioManager2.instance;
        if (audiomanager == null)
        {
            Debug.LogError("No audio manager in the scene");
        }
        key.enabled = false;
        collectedKey = false;
        cageMove = false;

}

private void Update()
    {
        Debug.Log("lives:" + _lives);
        lifeLeft.text = "x " + _lives;
        if (transform.position.x < endframePos.position.x)
        {

            if (!audiomanager.isplaying("nightAmbient"))
            {
                audiomanager.Play("nightAmbient");
            }
            else if (audiomanager.isplaying("castleAmbient"))
            {
                audiomanager.StopSound("castleAmbient");
            }

            
        }
        else if (transform.position.x > endframePos.position.x)
        {

            if (audiomanager.isplaying("nightAmbient"))
            {
                audiomanager.StopSound("nightAmbient");
            }
            else if (!audiomanager.isplaying("castleAmbient"))
            {
                audiomanager.Play("castleAmbient");
            }

        }
        //livesLeft();
        //if (cageDoor.position == CageTarget.position)
        //{

        //    gameOverScreen();

        //}
        if (cageMove)
        {
            cageDoor.position = Vector3.MoveTowards(cageDoor.position, CageTarget.position, cageDoorSpeed * Time.fixedDeltaTime);
            Invoke("gameOverButton", 5);
        }
    }

     public void takeDamage(int damage)
    {
        //play hurt animation
        //animat.SetTrigger("hurt");
        //shake.playShake(0.08f, 0.9f, 5f);

        if(damage == 10)
        {
            Debug.Log("burn damage!!!!!!");

        }

        
        //play hurt sound
        audiomanager.Play("hurt");
        currentHealth -= damage;
        healthBar.setHealthValue(currentHealth);

        hurtShake();
        //hurtpartcle();
        hurtFilter();
        //Invoke("hurtShake", 0.3f);
        //Invoke("hurtpartcle", 0.3f);
        if (currentHealth <= 0)
        {
            //Die();           
            Debug.Log("player died!!!!!!!!1");
            //Invoke("delPlayer", 1);
            delPlayer();
        }


    }
   
    void delPlayer()
    {
        //playerPos = gameObject.transform;
        //audiomanager.Play("iceExplosion");
        
        //Destroy(gameObject);
        
        Instantiate(effect, diepos.position, Quaternion.identity);
        //FindObjectOfType<gameOver>().GameOverScreen();
        //gameOver.GameOverScreen(score.dropCollected);
        FindObjectOfType<camerashake>().playShake(0.5f, 3f, 5f);
        gameObject.SetActive(false);

        if (_lives < 2)
        {
            gameOverScreen();
        }
        else
        {
            //livesLeft--;
           
            Invoke("reloadScene", 3);

        }

    }

    void hurtpartcle()
    {
        Instantiate(hurteffect, diepos.position, Quaternion.identity);
    }
    

    void hurtShake()
    {
        FindObjectOfType<camerashake>().playShake(0.3f, 1.5f, 5f);
    }

    void hurtFilter()
    {
        filterAnim.SetTrigger("hurtFilter");
    }
    //int livesLeft = 2;
    void reloadScene()
    {
        audiomanager.StopSound("spiderbossgrowl");
        _manager.playerDie();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    

    public void gameOverScreen()
    {
        //livesLeft = maxLives;
        stopAllSound();
        gameoverCanvas.SetActive(true);
        pauseButton.SetActive(false);
        //Time.timeScale = 0;
    }

    public void gameOverButton()
    {
        stopAllSound();
        _manager.livesLeft = _manager.maxLives;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    void stopAllSound()
    {
        audiomanager.StopSound("spiderbossgrowl");
        audiomanager.StopSound("theme");
        audiomanager.StopSound("spider3Sound");
        audiomanager.StopSound("spidermove");
        audiomanager.StopSound("castleAmbient");
        audiomanager.StopSound("run");
        audiomanager.Play("menu");
        audiomanager.Play("nightAmbient");
    }
}
