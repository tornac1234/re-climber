using UnityEngine;

public class ItemReference : MonoBehaviour
{
    public GameObject reference;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
        if (collision.TryGetComponent(out ItemPickUp itemPickUp))
        {
            itemPickUp.StumbleIntoItem(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("poiutydgsd");
        if (collision.TryGetComponent(out ItemPickUp itemPickUp))
        {
            itemPickUp.EscapeItemRange(this);
        }
    }
}
