using UnityEngine;
using UnityEngine.UI;

public class inGameUI : MonoBehaviour
{
    public const float width = 750f;

    public Image staminaBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        staminaBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0.5f * width);
    }

    public void staminaUpdate(float staminaRatio)
    {
        staminaBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, staminaRatio * width);
    }
}
