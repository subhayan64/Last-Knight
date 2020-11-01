using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour
{
    public Animator TransitonAnim;
    public float seconds = 1f;

        // Update is called once per frame
    void Update()
    {
        
    }

    public void loadLevel()
    {
        StartCoroutine(WaitForTransition(SceneManager.GetActiveScene().buildIndex + 1));            
    }

    IEnumerator WaitForTransition(int levelIndex)
    {
        TransitonAnim.SetTrigger("start");

        yield return new WaitForSeconds(seconds);

        SceneManager.LoadScene(levelIndex);
    }
}


