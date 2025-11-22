using UnityEngine;
using UnityEngine.UI;

public class Climbing : MonoBehaviour
{
    public bool IsClimbing;
    public Stamina stamina;
    public float climbingSpeed = 100f;
    public Movement movement;
    public Rigidbody2D rb;
    public float velocityMag => movement.rb.linearVelocity.magnitude;

    private void Start()
    {
        stamina.noStamina += NoStamina;
    }

    public void OnDestroy()
    {
        stamina.noStamina -= NoStamina;
    }

    public void SetClimbing(bool isClimbing)
    {
        /*
         * Sets IsClimbing field + removes gravity if climbing
         */ 
        IsClimbing = isClimbing;
        movement.enabled = !isClimbing;
        rb.gravityScale = isClimbing ? 0f : 1f;
    }

    private void NoStamina()
    {
        SetClimbing(false);
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
