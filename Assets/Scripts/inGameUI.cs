using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class inGameUI : MonoBehaviour
{

    // Gestion largeur barre de stam
    public float maxWidth = 750f;
    public float aimWidth;
    public float lastWidth;
    
    // Variables de temps pour faire la barre de stam smooth
    public float timeToStam = 1.5f; // S'update en timeToStam secondes
    public float timeLasting;

    public Image staminaBar;

    public Image itemSlot;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timerCurrentText;
    public float chronoTotal = 0f;
    public float chronoCurrent = 0f;
    public bool timeStopped;

    public Inventory inventory;

    public Stamina stamina;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemSlot.enabled = false;
        aimWidth = maxWidth;
        maxWidth = staminaBar.GetComponent<RectTransform>().sizeDelta.x;
        Inventory.OnPickup += itemPickUp;
        Inventory.OnRemove += itemUse;
        respawn.OnRespawn += timeReset;
        respawn.OnDeath += timeStop;
        timeResume();
    }


    /*
     * Call to this function to visually change the stamina bar
     */
    public void staminaUpdate(float staminaRatio)
    {
        if (aimWidth != staminaRatio * maxWidth)
        {
            aimWidth = staminaRatio * maxWidth;
            timeLasting = 0f;
            lastWidth = staminaBar.GetComponent<RectTransform>().sizeDelta.x;
        }
    }
    
    private void Update()
    {
        if (timeLasting < timeToStam)
        {
            timeLasting += Time.deltaTime;
            staminaBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Lerp(lastWidth, aimWidth, timeLasting / timeToStam));
        }
        staminaUpdate(stamina.value);

        chronoTotal += Time.deltaTime;
        timerText.text = Mathf.Floor(chronoTotal).ToString();
        if (!timeStopped)
        {
            chronoCurrent += Time.deltaTime;
            timerCurrentText.text = Mathf.Floor(chronoCurrent).ToString();
        }
    }

    public void itemPickUp(GameObject _)
    {
        itemSlot.enabled = true;
        itemSlot.sprite = inventory.Slots[0].VisibleSprite;
    }

    public void itemUse(int _)
    {
        itemSlot.enabled = false;
    }

    public void timeReset()
    {
        chronoCurrent = 0f;
        timeResume();
    }

    public void timeStop()
    {
        timeStopped = true;
    }
    public void timeResume()
    {
        timeStopped = false;
    }
}
