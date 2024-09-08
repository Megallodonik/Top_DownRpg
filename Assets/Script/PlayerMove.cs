using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] float speed = 5;
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
        Debug.Log(direction);
        rb.MovePosition(rb.position + speed * direction * Time.deltaTime);

        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}