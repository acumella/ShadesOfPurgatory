using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInstruction : MonoBehaviour
{
    private GameObject instructions, instructionToShow;
    [SerializeField] private int numInstruction;
    bool showing = false;
    private static bool[] shown = {false, false, false};

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !shown[numInstruction-1])
        {
            instructions = GameObject.Find("oldCanvas").transform.Find("Instructions").gameObject;
            instructions.SetActive(true);

            instructionToShow = instructions.transform.GetChild(numInstruction - 1).gameObject;
            instructionToShow.SetActive(true);

            Time.timeScale = 0;

            GameObject.Find("oldCanvas").GetComponent<PauseMenu>().onMainMenu = true;

            showing = true;
        }
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) && showing)
        {
            Time.timeScale = 1;
            instructions.SetActive(false);
            instructionToShow.SetActive(false);
            GameObject.Find("oldCanvas").GetComponent<PauseMenu>().onMainMenu = false;
            shown[numInstruction-1] = true;
            showing = false;
        }
    }
    
}
