using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionHandler : MonoBehaviour
{
    public float timeToFade = 1f;
    public int sceneToLoad = 1;
    public Animator anim;
    public SoundManager sm;

    private void Start()
    {
        sm.playSound(7);
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    public void RestartLevel()
    {
        StartCoroutine(ReloadLevel());
    }

    IEnumerator LoadLevel()

    {
        sm.playSound(0);

        anim.SetTrigger("Fadeout");

        sm.playSound(7);

        yield return new WaitForSeconds(1f);

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    IEnumerator ReloadLevel()
    {
        sm.playSound(0);


        anim.SetTrigger("Fadeout");

        sm.playSound(7);


        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
