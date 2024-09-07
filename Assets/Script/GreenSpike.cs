using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSpike : Obstacle
{
    public Collider2D Collider;

    private void Start()
    {
        Collider = GetComponent<Collider2D>();
        Invoke("EnableCollider", 1f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HitPlayer(-1);
        }
    }
    private void EnableCollider()
    {
        Collider.enabled = true;
    }
}
