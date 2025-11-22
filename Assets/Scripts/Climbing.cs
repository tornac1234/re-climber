using UnityEngine;

public class Climbing : MonoBehaviour
{
    public bool IsClimbing;
    public float climbingSpeed = 100f;
    public Movement movement;
    public Rigidbody2D rb;
    public float velocityMag => movement.rb.linearVelocity.magnitude;

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
        float horizontalMovement = Input.GetAxis("ClimbHorizontal");
        float verticalMovement = Input.GetAxis("ClimbVertical");
        Vector2 direction = new Vector2(horizontalMovement, verticalMovement);
        
        direction.Normalize();

        MovePlayer(direction);
    }
    private void MovePlayer(Vector2 direction)
    {
        movement.rb.linearVelocity = climbingSpeed * Time.deltaTime * direction;
    }
}
