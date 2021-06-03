    using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateBoundaries : MonoBehaviour
{
    CinemachineConfiner confiner;
    CinemachineVirtualCamera vcam;

    void Start()
    {
        confiner = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineConfiner>();
        confiner.InvalidatePathCache();
        confiner.m_BoundingShape2D = null;
        confiner.m_BoundingShape2D = GameObject.FindGameObjectWithTag("Boundaries").GetComponent<PolygonCollider2D>();


        //vcam = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineVirtualCamera>();
        //vcam.Follow = null;
        //vcam.Follow = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


}
