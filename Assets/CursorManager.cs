using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    //private SpriteRenderer rend;
    private Animator anim;

    //public Sprite cursor;
    //public Sprite cursorLeftClicked;
    //public Sprite cursorRightClicked;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Start()
    {
        //rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        ChangeCursorToDefault();
    }

    private void Update()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
    }

    private void ChangeCursor(int cursorType)
    {
        //rend.sprite = cursorType;
        anim.SetInteger("state", cursorType);
    }

    public void ChangeCursorToDefault()
    {
        ChangeCursor(0);
    }

    public void ChangeCursorOnLeftClick()
    {
        ChangeCursor(1);
    }

    public void ChangeCursorOnRightClick()
    {
        ChangeCursor(2);
    }


}
