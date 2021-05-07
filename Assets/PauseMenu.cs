using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool isPaused = false;
    public bool showingInstruction = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !showingInstruction) {
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
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
        Destroy(GameObject.Find("oldspook"));
        Destroy(GameObject.Find("oldCanvas"));
        Destroy(GameObject.Find("oldMain Camera"));
        Destroy(GameObject.Find("oldCursor"));
        Destroy(GameObject.Find("oldEventSystem"));
        Destroy(GameObject.Find("oldAudioManager"));

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
