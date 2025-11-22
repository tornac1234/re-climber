using UnityEngine;

public class ItemReference : MonoBehaviour
{
    public GameObject reference;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TriggEnter");
        if (collision.TryGetComponent(out ItemPickUp itemPickUp))
        {
            itemPickUp.StumbleIntoItem(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("TriggExit");
        if (collision.TryGetComponent(out ItemPickUp itemPickUp))
        {
            itemPickUp.EscapeItemRange(this);
        }
    }
}
