using UnityEngine;
using UnityEngine.UI;

public class inGameUI : MonoBehaviour
{
    public float maxWidth = 750f;

    public Image staminaBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxWidth = staminaBar.GetComponent<RectTransform>().sizeDelta.x;
        staminaUpdate(0.2f);
    }

    public void staminaUpdate(float staminaRatio)
    {
        staminaBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, staminaRatio * maxWidth);
    }
}
