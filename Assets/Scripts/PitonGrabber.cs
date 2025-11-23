using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PitonGrabber : MonoBehaviour
{
    public List<Piton> closePitons;
    public Piton closestPiton;
    public Climbing climbing;
    public Movement movement;
    public Rigidbody2D rb;

    public bool isGrabbing => grabbedPiton;
    public Piton grabbedPiton;

    public float GrabSpeed = 1f;
    public Vector2 GrabOffset;

    public void Update()
    {
        ManageInput();
        if (closePitons.Count == 0)
        {
            if (closestPiton)
            {
                DeselectPiton();
                closestPiton = null;
            }
            return;
        }

        Piton closest = FindClosestPiton();

        if (closest != closestPiton)
        {
            if (closestPiton)
            {
                DeselectPiton();
            }
            SelectPiton(closest);
            closestPiton = closest;
        }
    }

    public void FixedUpdate()
    {
        if (!grabbedPiton)
        {
            return;
        }
        transform.position = Vector2.Lerp(transform.position, grabbedPiton.transform.position + (Vector3)GrabOffset, Time.fixedDeltaTime * GrabSpeed);
    }

    public void SelectPiton(Piton piton)
    {
        piton.GetComponent<SpriteRenderer>().material.SetInt("_IsActive", 1);
    }

    public void DeselectPiton()
    {
        closestPiton.GetComponent<SpriteRenderer>().material.SetInt("_IsActive", 0);
    }

    public Piton FindClosestPiton()
    {
        Piton closest = closePitons[0];
        float minDistance = Vector2.Distance(transform.position, closest.transform.position);
        foreach (Piton piton in closePitons)
        {
            float distance = Vector2.Distance(transform.position, piton.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = piton;
            }
        }

        return closest;
    }

    public void ManageInput()
    {
        if (closestPiton && Input.GetButtonDown("Interact"))
        {
            if (isGrabbing)
            {
                UngrabClosest();
            }
            else
            {
                GrabClosest();
            }
        }
    }

    public void GrabClosest()
    {
        grabbedPiton = closestPiton;
        // TODO: add animation to go towards closest
        climbing.enabled = false;
        movement.enabled = false;
        rb.gravityScale = 0f;
        closestPiton.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.green);
    }

    public void UngrabClosest()
    {
        grabbedPiton = null;
        climbing.enabled = true;
        movement.enabled = true;
        rb.gravityScale = 1f;
        closestPiton.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.yellow);
        // TODO: switch back to normal animation
    }
}
