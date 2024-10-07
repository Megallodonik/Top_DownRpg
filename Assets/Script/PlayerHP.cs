using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] GameObject MenuContorller;
    [SerializeField] MenuContorller M_ControllerSc;
    public int Player_hp = 1;

    private void Start()
    {

    }

    // Start is called before the first frame update
    private void OnEnable()
    {

        Boss.ChangePlayerHpBoss += playerHP_update;
        Obstacle.ChangePlayerHpObstacle += playerHP_update;
    }
    private void playerHP_update(int hpChange)
    {
        Player_hp += hpChange;


    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(MenuContorller);
        Debug.Log(M_ControllerSc);
        Debug.Log(Player_hp);
        if (Player_hp <= 0)
        {
            M_ControllerSc.OnDead();

        }
    }
}
