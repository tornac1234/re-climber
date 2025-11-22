using UnityEngine;
using UnityEngine.UI;

public class Climbing : MonoBehaviour
{
    public bool IsClimbing;
    public Stamina stamina;
    public float climbingSpeed = 100f;
    public float dashSpeedMultiplier = 3f;
    public float dashDuration = 0.4f;
    public float dashStaminaMultiplier = 1.5f;
    private float dashStartingStamina;
    public Movement movement;
    public Rigidbody2D rb;
    public bool IsPiton = false;
    public bool IsRope = false;
    public bool IsDashing = false;
    public Vector2 DashDirection;
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
        movement.IsGrounded = false;
        rb.gravityScale = isClimbing ? 0f : 1f;
    }

    public void SetDashing(bool isDashing)
    {
        IsDashing = isDashing;
        if (isDashing)
        {
            DashDirection = GetNormalizedDirection();
            IsDashing = true;
            dashStartingStamina = stamina.value;
        }
    }

    private void NoStamina()
    {
        SetClimbing(false);
        if (IsDashing)
        {
            SetDashing(false);
        }
    }

    public Vector2 GetNormalizedDirection()
    {
        /*
         * Returns normalized climbing direction using input axis
         */
        float horizontalMovement = Input.GetAxis("ClimbHorizontal");
        float verticalMovement = Input.GetAxis("ClimbVertical");
        Vector2 direction = new Vector2(horizontalMovement, verticalMovement);

        return direction.normalized;
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
        if (!IsDashing && Input.GetButtonUp("Climb"))
        {
            SetClimbing(false);
            return;
        }
        if (!IsDashing && Input.GetButtonDown("Jump"))
        {
            SetDashing(true);
        }
        if (IsDashing)
        {
            if (dashStartingStamina - stamina.value >= dashDuration * dashSpeedMultiplier * dashStaminaMultiplier / stamina.timeDeplete)
            {
                SetDashing(false);
                if (!Input.GetButton("Climb"))
                {
                    SetClimbing(false);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (!IsClimbing)
        {
            return;
        }

        Vector2 direction = IsDashing ? DashDirection : GetNormalizedDirection();
        MovePlayer(direction);
    }
    private void MovePlayer(Vector2 direction)
    {
        float factorDash = IsDashing ? dashSpeedMultiplier : 1f;
        movement.rb.linearVelocity = climbingSpeed * factorDash * Time.deltaTime * direction;
    }
}
