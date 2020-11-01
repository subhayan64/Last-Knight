using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class maninMenu : MonoBehaviour
{

    private AudioManager2 audiomanager;
    public levelLoader loader;

    // Start is called before the first frame update
    void Start()
    {
        audiomanager = AudioManager2.instance;
        if (audiomanager == null)
        {
            Debug.LogError("No audio manager in the scene");
        }
        audiomanager.Play("menu");
        audiomanager.Play("nightAmbient");
        Time.timeScale = 1;
    }
    public void loadNextScene()
    {
        audiomanager.StopSound("menu");
        //audiomanager.StopSound("nightAmbient");
        loader.loadLevel();
    }


}
