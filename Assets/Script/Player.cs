using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    



    Vector2 mousePos;
    private Camera cam;

    void Start()
    {
        
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse1))
        //{
        //    mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //    Debug.Log("кнопка");
        //    agent.SetDestination(mousePos);
            
        //}

    }



}
