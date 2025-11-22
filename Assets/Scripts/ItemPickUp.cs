using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public List<ItemReference> itemsAround = new();
    private ItemReference closestItem;
    public Inventory inventory;

    public void StumbleIntoItem(ItemReference itemRef)
    {
        itemsAround.Add(itemRef);
    }

    public void EscapeItemRange(ItemReference itemRef)
    {
        itemRef.GetComponent<SpriteRenderer>().material.SetInt("_IsActive", 0);
        itemsAround.Remove(itemRef);
    }

    private void UpdateClosestItem()
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
        if (minItem == closestItem) return;
        if (closestItem != null)
        {
            closestItem.GetComponent<SpriteRenderer>().material.SetInt("_IsActive", 0);
        }
        closestItem = minItem;
        closestItem.GetComponent<SpriteRenderer>().material.SetInt("_IsActive", 1);
    }

    private void FixedUpdate()
    {
        if (itemsAround.Count == 0)
        {
            closestItem = null;
            return;
        }
        UpdateClosestItem();
    }
    
    void Update()
    {
        if (closestItem == null) return;
        if (Input.GetButtonDown("Interact") && inventory.PickupItem(closestItem.reference))
        {
            itemsAround.Remove(closestItem);
            Destroy(closestItem.gameObject);
            UpdateClosestItem();
        }
    }
}
