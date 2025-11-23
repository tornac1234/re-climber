using UnityEngine;

public class Rope : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out RopeGrabber ropeGrabber))
        {
            ropeGrabber.contactRopes.Add(this);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out RopeGrabber ropeGrabber))
        {
            ropeGrabber.contactRopes.Remove(this);
        }
    }
}
