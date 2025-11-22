using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public const int SIZE = 1;
    public List<ItemSlot> Slots;

    public UnityAction<GameObject> OnPickup;
    public UnityAction<int> OnRemove;

    public void Start()
    {
        ResetInventory();
    }

    public bool CanPickup()
    {
        return Slots.Any(slot => !slot.IsOccupied);
    }

    public bool PickupItem(GameObject itemPrefab)
    {
        // Early detection to avoid problems, even though the check should be handled by the calling script
        if (!CanPickup())
        {
            return false;
        }

        ItemSlot slot = Slots.First(slot => !slot.IsOccupied);
        Debug.Log($"slot: {slot.Index}");

        slot.SetPrefab(itemPrefab);

        OnPickup?.Invoke(itemPrefab);

        return true;
    }

    public bool RemoveItem(int slotIndex)
    {
        return RemoveItem(Slots[slotIndex]);
    }

    /// <returns><c>true</c> if there was an item to be removed</returns>
    public bool RemoveItem(ItemSlot itemSlot)
    {
        if (!itemSlot.IsOccupied)
        {
            return false;
        }

        itemSlot.SetPrefab(null);
        OnRemove?.Invoke(itemSlot.Index);
        return true;
    }

    public void ResetInventory()
    {
        Slots = new();
        for (int i = 0; i < SIZE; i++)
        {
            Slots.Add(new(i));
        }
    }

    public class ItemSlot
    {
        public GameObject Prefab;
        public int Index;

        public Sprite VisibleSprite;
        public bool IsOccupied => Prefab;

        public ItemSlot(int index)
        {
            Index = index;
        }

        public void SetPrefab(GameObject prefab)
        {
            Prefab = prefab;

            if (Prefab)
            {
                VisibleSprite = prefab.GetComponent<Item>().Data.InventorySprite;
            }
            else
            {
                VisibleSprite = null;
            }
        }
    }
}
