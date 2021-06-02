using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInstruction : MonoBehaviour
{
    private GameObject instructions, instructionToShow;
    [SerializeField] private int numInstruction;
    private GameObject mouse;
    bool showing = false;
    private static bool[] shown = {false, false, false};

    private void Start()
    {
        mouse = GameObject.FindGameObjectWithTag("Mouse");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !shown[numInstruction-1])
        {
            Cursor.visible = true;
            mouse.SetActive(false);

            instructions = GameObject.Find("oldCanvas").transform.Find("Instructions").gameObject;
            instructions.SetActive(true);

            instructionToShow = instructions.transform.GetChild(numInstruction - 1).gameObject;
            instructionToShow.SetActive(true);

            Time.timeScale = 0;

            GameObject.Find("oldCanvas").GetComponent<PauseMenu>().showingInstruction = true;

            showing = true;

            GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.instructionsShown.Add(gameObject.name);
        }
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.F)) && showing)
        {
            mouse.SetActive(true);
            Time.timeScale = 1;
            instructions.SetActive(false);
            instructionToShow.SetActive(false);
            shown[numInstruction-1] = true;
            Cursor.visible = false;
            showing = false;
            GameObject.Find("oldCanvas").GetComponent<PauseMenu>().showingInstruction = false;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < 3; i++){
            shown[i] = false;
        }
    }
}
