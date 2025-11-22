using Unity.VisualScripting;
using UnityEngine;

public class ItemReference : MonoBehaviour
{
    public GameObject reference;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ItemPickUp itemPickUp))
        {
            Debug.Log("poijhkugfc");
            itemPickUp.StumbleIntoItem(this);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ItemPickUp itemPickUp))
        {
            Debug.Log("poiutydgsd");
            itemPickUp.EscapeItemRange(this);
        }
    }
}
