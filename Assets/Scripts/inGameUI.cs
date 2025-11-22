using UnityEngine;
using UnityEngine.UI;

public class inGameUI : MonoBehaviour
{
    public float maxWidth = 750f;
    public float aimWidth;
    public float lastWidth;

    public float timeToStam = 2f;
    public float timeLasting;

    public Image staminaBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aimWidth = maxWidth;
        maxWidth = staminaBar.GetComponent<RectTransform>().sizeDelta.x;
        staminaUpdate(0.2f);
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
}
