using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed = 5f;
    public float climbSpeed = 4f;
    public float gravity = 3f;
    public float jumpForce = 8f;

    public bool IsClimbing ;
    public bool IsGrounded ;
    public bool IsJumping ;

    public Vector2 velocity = Vector2.zero;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && (IsGrounded || IsClimbing))
        {
            IsJumping = true;
        }

        MovePlayer(horizontalMovement);
    }

    void MovePlayer(float horizontalMovement)
    {
        rb.linearVelocity = new Vector2(horizontalMovement, rb.linearVelocity.y);

        if (IsJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce) );
            IsJumping = false;
        }
    }
}
