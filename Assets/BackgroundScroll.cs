using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float speed = 10000f;
    private RectTransform rtransform;
    private Vector3 startPos;

    private bool moved = false;

    void Start()
    {
        rtransform = GetComponent<RectTransform>();
        startPos = rtransform.localPosition;
        Debug.Log(startPos);
    }

    void Update()
    {
        if (!moved)
        {
            rtransform.Translate(Vector3.right * speed * Time.deltaTime);
            if (rtransform.localPosition.x > 2407f)
            {
                Debug.Log(rtransform.position);
                //moved = true;
                rtransform.localPosition = startPos;
            }
        }
    }
}
