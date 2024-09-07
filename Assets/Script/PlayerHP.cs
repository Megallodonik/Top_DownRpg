using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int Player_hp = 1;

 
    
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
        Debug.Log(Player_hp);
        
    }
}
