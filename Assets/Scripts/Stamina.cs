using UnityEngine;
using UnityEngine.Events;

public class Stamina : MonoBehaviour
{
    public Movement Movement;
    public float value = 1F;
    public float timeDeplete = 10F;
    public float timeRefill = 5F;
    public bool isRest = false;
    public UnityAction noStamina;

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        bool isClimbing = Movement.IsClimbing;
        bool isResting = false; // à relier à Movement.cs. Pour l'instant, on utilise isRest à la place (variable publique modifiable en live)
        if (isClimbing)
        {
            value = Mathf.Clamp(value - dt / timeDeplete, 0F, 1F);
            if (value == 0F)
            {
                noStamina.Invoke();
            }
        }
        else if (isRest)
        {
            if (value < 1.0)
            {
                value = Mathf.Clamp(value + dt / timeRefill, 0F, 1F);
            }
        }
    }
}
