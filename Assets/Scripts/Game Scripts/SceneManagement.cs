using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    public Animator transition;
    public void ResetCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Destroy(gameManager);
    }

    public void ReturnToMenuScreen()
    {
        Time.timeScale = 1f;
        StartCoroutine(ReturnToMenu());
    }

    IEnumerator ReturnToMenu()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
        Destroy(gameManager);
    }
}
