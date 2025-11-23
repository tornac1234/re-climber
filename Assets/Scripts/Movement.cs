using UnityEngine;

public class Movement : MonoBehaviour
{
    public int GroundLayerMask;

    public float speed = 100f;
    public AudioSource walkCue;
    private float playWalkCueVelocityThreshold = 0.25F;

    public float jumpForce = 5f;
    public float JumpCooldown = 0.5f;
    public AudioSource jumpCue;
    private float lastJump = -1f;

    public bool IsGrounded;
    public bool IsJumping;
    
    public Transform groundCheckRight;
    public Transform groundCheckLeft;

    public Vector2 velocity;
    public Rigidbody2D rb;
    public Climbing climbing;
    public Stamina stamina;

    void Start()
    {
        GroundLayerMask = 1<<LayerMask.NameToLayer("Ground");
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        IsGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position, GroundLayerMask);
        if (IsGrounded && !walkCue.isPlaying && rb.linearVelocity.magnitude > playWalkCueVelocityThreshold)
        {
            walkCue.Play();
        } else if (walkCue.isPlaying && (!IsGrounded || rb.linearVelocity.magnitude < playWalkCueVelocityThreshold))
        {
            walkCue.Pause();
        }

        if (!IsJumping && (Time.time - lastJump >= JumpCooldown) && Input.GetButtonDown("Jump") && IsGrounded && stamina.consumeStamina(0.1f))
        {
            IsJumping = true;
            lastJump = Time.time;
        }        
    }

    public void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        MovePlayer(horizontalMovement);
    }

    private void MovePlayer(float horizontalMovement)
    {
        if (IsJumping)
        {
            jumpCue.Play();
            rb.AddForce(new Vector2(horizontalMovement, jumpForce), ForceMode2D.Impulse);
                                                              
            IsJumping = false;
        }
        if (IsGrounded)
        {
            rb.linearVelocity = new Vector2(horizontalMovement, rb.linearVelocity.y);
        }
    }
}
