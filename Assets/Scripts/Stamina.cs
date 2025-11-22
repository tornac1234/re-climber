using UnityEngine;
using UnityEngine.Events;

public class Stamina : MonoBehaviour
{
    public Movement Movement;
    public float value = 1.0F;
    public float timeDeplete = 10.0F;
    public float timeRefill = 5.0F;
    public bool isRest = false;
    public UnityAction noStamina;

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        bool isClimbing = Movement.IsClimbing;
        bool isResting = false; // à fix
        if (isClimbing)
        {
            value -= dt / timeDeplete;
            if (value <= 0)
            {
                noStamina.Invoke();
                value = 0;
            }
        }
        else if (isRest)
        {
            if (value < 1.0)
            {
                value += dt / timeRefill;
            }
        }
    }
}
