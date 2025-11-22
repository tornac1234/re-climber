using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ItemPickUp : MonoBehaviour
{
    private const string giverTag = "Giver";
    private readonly List<ItemReference> itemsAround = new ();
    private ItemReference closestItem;

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

    private void updateClosestItem()
    {
        ItemReference minItem = itemsAround[0];
        float minDist = float.MaxValue;
        foreach (ItemReference item in itemsAround)
        {
            float newDist = Vector2.Distance(transform.position, item.transform.position);
            if (newDist < minDist) 
            {
                minItem = item;
                minDist = newDist;
            }
        }
    }

    private void FixedUpdate()
    {
        if (itemsAround.Count == 0)
        {
            closestItem = null;
            return;
        }
        updateClosestItem();
    }
    // Update is called once per frame
    void Update()
    {
        if (itemsAround.Count == 0) return;
        // Method to outline object
        if (Input.GetButton("Interact"))
        {
            // [Method to pick up closest item]
            itemsAround.Remove(closestItem);
            updateClosestItem();
        }
    }
}
