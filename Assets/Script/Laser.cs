using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Obstacle
{
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        StartCoroutine(ScaleChange());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HitPlayer(-1);
        }
    }
    private IEnumerator ScaleChange()
    {
        for (float i = 0; transform.localScale.x < 5; i += 0.05f)
        {
            Debug.Log(i);
            Debug.Log(transform.localScale.x);
            Vector3 scaleChange = new Vector3(i, 0, 0);
            transform.localScale += scaleChange;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
