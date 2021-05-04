using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool isPaused = false;
    public bool onMainMenu = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !onMainMenu) {
            if (isPaused) Resume();
            else Pause();
        }    
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ReturnToMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level != 0)
        {
            onMainMenu = false;
            Cursor.visible = false;
        }
        else
        {
            onMainMenu = true;
            Cursor.visible = true;
        }
    }

}
