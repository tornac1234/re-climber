using UnityEngine;
using UnityEngine.UI;

public class inGameUI : MonoBehaviour
{

    // Gestion largeur barre de stam
    public float maxWidth = 750f;
    public float aimWidth;
    public float lastWidth;

    // Variables de temps pour faire la barre de stam smooth
    public float timeToStam = 1f; // S'update en timeToStam secondes
    public float timeLasting;

    public Image staminaBar;

    public Image itemSlot;

    public Inventory inventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemSlot.enabled = false;
        aimWidth = maxWidth;
        maxWidth = staminaBar.GetComponent<RectTransform>().sizeDelta.x;
        Inventory.OnPickup += itemPickUp;
        Inventory.OnRemove += itemUse;
        
    }


    /*
     * Call to this function to visually change the stamina bar
     */
    public void staminaUpdate(float staminaRatio)
    {
        aimWidth = staminaRatio * maxWidth;
        timeLasting = 0f;
        lastWidth = staminaBar.GetComponent<RectTransform>().sizeDelta.x;

    }
    
    private void Update()
    {
        if (timeLasting < timeToStam)
        {
            timeLasting = timeLasting + Time.deltaTime;
            staminaBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Lerp(lastWidth, aimWidth, timeLasting));
        }
    }

    public void itemPickUp(GameObject _)
    {
        itemSlot.enabled = true;
        itemSlot.GetComponent<SpriteRenderer>().sprite = inventory.Slots[0].VisibleSprite;
    }

    public void itemUse(int _)
    {
        itemSlot.enabled = false;
    }
}
