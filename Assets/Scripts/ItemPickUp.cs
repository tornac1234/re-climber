using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public List<ItemReference> itemsAround = new();
    private ItemReference closestItem;

    public void StumbleIntoItem(ItemReference itemRef)
    {
        itemsAround.Add(itemRef);
    }

    public void EscapeItemRange(ItemReference itemRef)
    {
        itemsAround.Remove(itemRef);
    }

    private void updateClosestItem()
    {
        if (itemsAround.Count == 0)
        {
            closestItem = null;
            return;
        }
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
        closestItem = minItem;
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
            Destroy(closestItem.gameObject);
            updateClosestItem();
        }
    }
}
