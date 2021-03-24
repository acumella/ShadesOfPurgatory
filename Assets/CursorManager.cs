using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursor;
    public Texture2D cursorClicked;

    void Awake()
    {
        ChangeCursorToDefault();
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void ChangeCursor(Texture2D cursorType)
    {
        Vector2 hotspot = new Vector2(cursorType.width / 2, cursorType.height / 2);
        Cursor.SetCursor(cursorType, hotspot, CursorMode.Auto);
    }

    public void ChangeCursorToDefault()
    {
        ChangeCursor(cursor);
    }

    public void ChangeCursorOnClick()
    {
        ChangeCursor(cursorClicked);
    }


}
