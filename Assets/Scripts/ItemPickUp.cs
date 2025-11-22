using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private const string giverTag = "Giver";
    private readonly List<ItemReference> itemsAround = new ();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(giverTag))
        {
            itemsAround.Add(collision.GetComponent<ItemReference>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(giverTag))
        {
            itemsAround.Remove(collision.GetComponent<ItemReference>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
