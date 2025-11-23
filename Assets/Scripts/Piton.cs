using UnityEngine;

public class Piton : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PitonGrabber pitonGrabber))
        {
            pitonGrabber.closePitons.Add(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PitonGrabber pitonGrabber))
        {
            pitonGrabber.closePitons.Remove(this);
        }
    }
}
