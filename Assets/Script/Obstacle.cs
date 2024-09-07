using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static event Action<int> ChangePlayerHpObstacle;

    // Start is called before the first frame update

    protected void HitPlayer(int dmg)
    {
        ChangePlayerHpObstacle?.Invoke(dmg);
    }
}
