using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TrentProtectorBoss;
using static UnityEngine.GraphicsBuffer;

public class Boss : MonoBehaviour
{
   

    public static event Action<int> ChangePlayerHpBoss;

    // Start is called before the first frame update
    
    protected void HitPlayer(int dmg)
    {
        ChangePlayerHpBoss?.Invoke(dmg);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
