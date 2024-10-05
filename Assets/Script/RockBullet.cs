using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBullet : Obstacle
{
    [SerializeField] float speed = 50f;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject Player;
    Rigidbody2D rb;
    Vector2 direction;
    Animator anim;

    public Collider2D Collider;

    private void Start()
    {
        StartCoroutine(timer());
        Collider = GetComponent<Collider2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        direction = Player.transform.position;
    }
    private void OnEnable()
    {

    }
    private IEnumerator timer()
    {
        yield return new WaitForSeconds(15f);
        this.gameObject.SetActive(false);
        Collider.enabled = false;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HitPlayer(-1);
        }
    }

    private void FixedUpdate()
    {

        rb.MovePosition(rb.position + speed * direction * Time.deltaTime);
    }
}
