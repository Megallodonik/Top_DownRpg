using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TurretRock : MonoBehaviour
{
    [SerializeField] GameObject RockBullet;
    [SerializeField] RockBoss RockBoss;
    // Start is called before the first frame update
    private void OnEnable()
    {
        
        StartCoroutine(Shoot());
    }
    private void FixedUpdate()
    {
        //transform.LookAt(Player.transform);
    }
    private IEnumerator Shoot()
    {
        
        for (int i = 0; i < 40; i++)
        {
            Instantiate(RockBullet, transform.position, new Quaternion(0, 0, 0, 0));
            
            yield return new WaitForSeconds(0.5f);
            if (i == 39)
            {
                RockBoss.ChoosingAttack();
            }

        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
