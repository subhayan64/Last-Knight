using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class intro_music : MonoBehaviour
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
    }

    public void playIntro()
    {
        audiomanager.Play("IntroMusic");
    }

    public void loadNextScene()
    {
        audiomanager.StopSound("IntroMusic");
        loader.loadLevel();
    }

    void playBirdSound()
    {
        audiomanager.Play("birdSound");
    }

    void playCroudNoise()
    {
        audiomanager.Play("crowdNoise");

    }

    void playSpiderChatter()
    {
        audiomanager.Play("spiderChatter");

    }

    void playkingShout()
    {
        audiomanager.Play("kingShout");

    }

    void playknightRun()
    {
        audiomanager.Play("knight_run");

    }
}
