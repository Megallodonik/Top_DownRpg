using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RockBullet : Obstacle
{
    [SerializeField] float speed = 800f;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject Player;
    Rigidbody2D rb;
    Vector2 direction;
    Animator anim;

    public Collider2D Collider;

    private void Start()
    {
        StartCoroutine(timer());
        StartCoroutine(timer_2());
        Collider = GetComponent<Collider2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        direction = new Vector2(Player.transform.position.x, Player.transform.position.y);
    }
    private void OnEnable()
    {

    }
    private IEnumerator timer_2()
    {
        yield return new WaitForSeconds(0.2f);
        if (transform.position.x == direction.x && transform.position.y == direction.y)
        {
            this.gameObject.SetActive(false);
            Collider.enabled = false;
        }
        else
        {
            StartCoroutine(timer_2());
        }
        

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

        float step = 0.2f;
        transform.position = Vector3.MoveTowards(transform.position, direction, step);
    }
}
