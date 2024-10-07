using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] float speed = 1f;
    [SerializeField] SpriteRenderer spriteRenderer;

    Rigidbody2D rb;
    Vector2 direction;
    Animator anim;
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        direction.x = joystick.Horizontal;
        direction.y = joystick.Vertical;

        //anim.SetFloat("move", Mathf.Max(Mathf.Abs(horizontal), Mathf.Abs(vertical)));
    }
    void FixedUpdate()
    {

        rb.MovePosition(rb.position + speed * direction * Time.deltaTime);


    }
}