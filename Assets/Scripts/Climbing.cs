using UnityEngine;

public class Climbing : MonoBehaviour
{
    public bool IsClimbing;
    public float climbingSpeed = 4f;
    public Movement movement;
    public Rigidbody2D rb;

    public void SetClimbing(bool isClimbing)
    {
        /*
         * Sets IsClimbing field + removes gravity if climbing
         */ 
        IsClimbing = isClimbing;
        movement.enabled = !isClimbing;
        rb.gravityScale = isClimbing ? 0f : 1f;
    }

    private void Update()
    {
        if (!IsClimbing)
        {
            if (Input.GetButtonDown("Climb") && movement.IsGrounded)
            {
                SetClimbing(true);
            }
            return;
        }
        if (Input.GetButtonUp("Climb"))
        {
            SetClimbing(false);
            return;
        }
    }

    void FixedUpdate()
    {
        if (!IsClimbing)
        {
            return;
        }

        // Get normalized climb direction
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(horizontalMovement, verticalMovement);
        // TODO: consume stamina
        direction.Normalize();

        MovePlayer(direction);
    }
    private void MovePlayer(Vector2 direction)
    {
        movement.rb.linearVelocity = climbingSpeed * Time.deltaTime * direction;
    }
}
