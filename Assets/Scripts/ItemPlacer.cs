using UnityEngine;

public class ItemPlacer : MonoBehaviour
{
    public Inventory inventory;

    // Let's say we only have one inventory slot
    public GameObject selectedPrefab;

    public Transform RopePlaceReference;
    public Transform PitonPlaceReference;

    public Climbing climbing;
    public Movement movement;

    public AudioSource audioSource;

    public void Start()
    {
        Inventory.OnPickup += SelectPrefab;
        Inventory.OnRemove += DeselectPrefab;
    }

    private void OnDestroy()
    {
        Inventory.OnPickup -= SelectPrefab;
        Inventory.OnRemove -= DeselectPrefab;
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
        ItemData itemData = selectedPrefab.GetComponent<Item>().Data;

        switch (itemData.Name)
        {
            case "Rope":
                if (!movement.IsGrounded)
                {
                    return;
                }
                break;
            case "Piton":
                if (!climbing.IsClimbing)
                {
                    return;
                }
                break;
        }

        GameObject instance = GameObject.Instantiate(selectedPrefab);

        inventory.RemoveItem(0);
        
        switch (itemData.Name)
        {
            case "Rope":
                instance.transform.position = RopePlaceReference.position;
                break;

            case "Piton":
                instance.transform.position = PitonPlaceReference.position;
                audioSource.Play();
                break;
        }
    }
}
