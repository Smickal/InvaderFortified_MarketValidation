using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject PauseMenuUI;
    public static bool gameIsPaused = false;
    void Start()
    {
        PauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape Key Pressed!");
            if(gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space is pressed");
        }
    }

    public void PauseGame()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
}
