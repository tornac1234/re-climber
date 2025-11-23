using System.Collections.Generic;
using UnityEngine;

public class PitonGrabber : MonoBehaviour
{
    public List<Piton> closePitons;
    public Piton closestPiton;
    public Climbing climbing;

    public bool isGrabbing;

    public void Update()
    {
        ManageInput();
        if (closePitons.Count == 0)
        {
            if (closestPiton)
            {
                DeselectPiton();
            }
            return;
        }

        Piton closest = FindClosestPiton();

        if (closestPiton != closest)
        {
            DeselectPiton();
        }
        SelectPiton(closest);
        closestPiton = closest;
    }

    public void SelectPiton(Piton piton)
    {
        piton.GetComponent<SpriteRenderer>().material.SetInteger("_IsActive", 1);
    }

    public void DeselectPiton()
    {
        closestPiton.GetComponent<SpriteRenderer>().material.SetInteger("_IsActive", 0);
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
        if (Input.GetButtonDown("Interact"))
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
        isGrabbing = true;
        // TODO: add animation to go towards closest
        climbing.enabled = false;
    }

    public void UngrabClosest()
    {
        isGrabbing = false;
        climbing.enabled = true;
        // TODO: switch back to normal animation
    }
}
