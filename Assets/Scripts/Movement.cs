using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed = 100f;
    public float gravity = 3f;
    public float jumpForce =5f;

    public bool IsClimbing;
    public bool IsGrounded;
    public bool IsJumping = false;
    
    public Transform groundCheckRight;
    public Transform groundCheckLeft;

    public Vector2 velocity = Vector2.zero;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        IsGrounded = Physics2D.OverlapArea(groundCheckLeft.position,groundCheckRight.position);
        float horizontalMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        
        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            IsJumping = true;
        }

        

        MovePlayer(horizontalMovement);
        
    }

    private void MovePlayer(float horizontalMovement)
    {

        rb.linearVelocity = new Vector2(horizontalMovement, rb.linearVelocity.y);
        
        if (IsJumping)
        {

            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            IsJumping = false;
        }
    }
}
