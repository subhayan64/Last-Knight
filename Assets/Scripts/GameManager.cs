using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private static bool _created = false;

    //Accessible only trough editor or from this class
    [SerializeField]
    public int maxLives = 3;
    [HideInInspector]
    public int livesLeft;
    private AudioManager2 audiomanager;
   
    private void Awake()
    {
        if (!_created)
        {
            DontDestroyOnLoad(this.gameObject);
            _created = true;
            Init();
        }
        Time.timeScale = 1;
    }

    private void Start()
    {
        audiomanager = AudioManager2.instance;
        if (audiomanager == null)
        {
            Debug.LogError("No audio manager in the scene");
        }
        audiomanager.Play("theme");
        
        
    }

  
    public void Init()
    {
        livesLeft = maxLives;
    }

    public void playerDie()
    {
        
            livesLeft--;        
        
    }

    public void PauseGame()
    {
        audiomanager.StopSound("theme");
        audiomanager.Play("menu");
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        audiomanager.StopSound("menu");
        audiomanager.Play("theme");
        Time.timeScale = 1;
        //StartCoroutine(waitSeconds(1));
    }

    

    
}