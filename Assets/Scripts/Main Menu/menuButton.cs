using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuButton : MonoBehaviour
{
    public Animator transition;

    public void ChangeToPlayScene()
    {
        StartCoroutine(LoadLevel(1));
    }

    public void ChangeToHowToScene()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(2).buildIndex);
    }

    public void ChangeToSettingScene()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(3).buildIndex);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    IEnumerator LoadLevel(int index)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(index);
    }
}
