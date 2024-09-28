using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSpike : Obstacle
{
    [SerializeField] float speed = 1;
    [SerializeField] SpriteRenderer spriteRenderer;

    Rigidbody2D rb;
    Vector2 direction;
    Animator anim;

    public Collider2D Collider;

    private void Start()
    {
        Collider = GetComponent<Collider2D>();
        Invoke("EnableCollider", 0.7f);
        rb = GetComponent<Rigidbody2D>();
        direction = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
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

    private void FixedUpdate()
    {
       
        rb.MovePosition(rb.position + speed * direction * Time.deltaTime);
    }
}
