using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;

    Animator anim;
    SpriteRenderer sr;

    [SerializeField]
    GameObject diaCanvas;


    public void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        if (diaCanvas.activeSelf) 
        {
            rb.velocity = Vector2.zero;
            return;
        }
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        anim.SetFloat("Speed", rb.velocity.magnitude);
        anim.SetFloat("UD", rb.velocity.y);
        sr.flipX = rb.velocity.x > 0;
    }
}
