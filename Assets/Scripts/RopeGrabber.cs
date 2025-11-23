using System.Collections.Generic;
using UnityEngine;

public class RopeGrabber : MonoBehaviour
{
    public List<Rope> contactRopes = new();

    public bool GrabbingRope => contactRopes.Count > 0;

    public bool isGrabbing;

    public Rigidbody2D rb;
    public Movement movement;
    public Climbing climbing;

    public Transform bottom;
    public Transform top;

    public GrabState state;

    public float Speed = 3f;

    public void Update()
    {
        if (!isGrabbing && state == GrabState.NONE && !movement.IsGrounded && Input.GetButtonDown("Interact") && GrabbingRope)
        {
            state = GrabState.TO_BOT;
            StartGrabbing();
        }
    }

    public void FixedUpdate()
    {
        if (state == GrabState.NONE)
        {
            return;
        }

        Vector2 goal = Vector2.zero;
        GrabState nextState = GrabState.NONE;

        switch (state)
        {
            case GrabState.TO_BOT:
                goal = bottom.transform.position;
                nextState = GrabState.TO_TOP;
                break;
            case GrabState.TO_TOP:
                goal = top.transform.position;
                nextState = GrabState.NONE;
                break;
        }
        Vector2 destination = Vector2.Lerp(transform.position, goal, Time.fixedDeltaTime * Speed);
        float distanceToGoal = Vector2.Distance((Vector2)transform.position, goal);

        if (distanceToGoal < 0.1)
        {
            state = nextState;
            if (state == GrabState.NONE)
            {
                StopGrabbing();
            }
        }
        transform.position = new(destination.x, destination.y, -8.94f);
    }

    public void StartGrabbing()
    {
        climbing.enabled = false;
        movement.enabled = false;

        bottom = contactRopes[0].transform.Find("bottom");
        top = contactRopes[0].transform.Find("top");
        
    }

    public void StopGrabbing()
    {
        climbing.enabled = true;
        movement.enabled = true;
    }

    public enum GrabState
    {
        NONE,
        TO_BOT,
        TO_TOP
    }
}
