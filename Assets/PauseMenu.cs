using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool isPaused = false;
    public bool showingInstruction = false;
    public GameObject pauseMenuUI;
    private GameData gd;

    private void Start()
    {
        gd = GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd;
    }

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

        //GameObject.Find("oldspook").SetActive(false);
        //GameObject.Find("oldCanvas").SetActive(false);
        //GameObject.Find("oldMain Camera").SetActive(false);
        //GameObject.Find("oldCursor").SetActive(false);
        //GameObject.Find("oldEventSystem").SetActive(false);
        //GameObject.Find("oldAudioManager").SetActive(false);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
