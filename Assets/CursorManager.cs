using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    private SpriteRenderer rend;
    
    public Sprite cursor;
    public Sprite cursorLeftClicked;
    public Sprite cursorRightClicked;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        ChangeCursorToDefault();
    }

    private void Update()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
    }

    private void ChangeCursor(Sprite cursorType)
    {
        rend.sprite = cursorType;
    }

    public void ChangeCursorToDefault()
    {
        ChangeCursor(cursor);
    }

    public void ChangeCursorOnLeftClick()
    {
        ChangeCursor(cursorLeftClicked);
    }

    public void ChangeCursorOnRightClick()
    {
        ChangeCursor(cursorRightClicked);
    }


}
