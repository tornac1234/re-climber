using UnityEngine;

public class AniimManager : MonoBehaviour
{
    public Movement movement;
    public Climbing climbing;
    public respawn resp;
    public Animator animator;
    public RopeGrabber ropeGrabber;

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("move", climbing.velocityMag > 0F);
        animator.SetBool("isClimbing", climbing.IsClimbing);
        animator.SetBool("jump", movement.jumpedAndInAir);
        animator.SetBool("isDead", resp.isDead);
        animator.SetBool("Dash", climbing.IsDashing);
        animator.SetBool("onPiton", climbing.IsPiton);
        animator.SetBool("onRope", ropeGrabber.GrabbingRope);
    }
}
