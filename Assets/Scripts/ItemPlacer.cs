using UnityEngine;

public class ItemPlacer : MonoBehaviour
{
    public Inventory inventory;

    // Let's say we only have one inventory slot
    public GameObject selectedPrefab;

    public Transform RopePlaceReference;
    public Transform PitonPlaceReference;

    public void Start()
    {
        inventory.OnPickup += SelectPrefab;
        inventory.OnRemove += DeselectPrefab;
    }

    private void OnDestroy()
    {
        inventory.OnPickup -= SelectPrefab;
        inventory.OnRemove -= DeselectPrefab;
    }

    public void SelectPrefab(GameObject prefab)
    {
        selectedPrefab = prefab;
    }

    public void DeselectPrefab(int _)
    {
        selectedPrefab = null;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Place"))
        {
            TryPlaceItem();
        }
    }

    public void TryPlaceItem()
    {
        if (!selectedPrefab)
        {
            return;
        }
        inventory.RemoveItem(0);

        GameObject instance = GameObject.Instantiate(selectedPrefab);
        
        ItemData itemData = instance.GetComponent<ItemData>();
        switch (itemData.Name)
        {
            case "Rope":
                instance.transform.position = RopePlaceReference.position;
                break;

            case "Piton":
                inventory.transform.position = PitonPlaceReference.position;
                break;
        }
    }
}
