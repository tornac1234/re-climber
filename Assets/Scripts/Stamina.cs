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

    public void setStamina(float newValue)
    {
        /*
         *  Set stamina value directly (in the range from 0. to 1.)
         */
        value = Mathf.Clamp(newValue, 0F, 1F);
    }

    public bool consumeStamina(float amount)
    {
        /*
         * Consumes given amount of stamina, if possible.
         * If it is possible, returns true, else returns false
         */
        if (value < amount)
        {
            return false;
        }
        setStamina(value - amount);
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        bool isClimbing = Movement.IsClimbing;
        bool isResting = false; // à relier à Movement.cs. Pour l'instant, on utilise isRest à la place (variable publique modifiable en live)
        if (isClimbing)
        {
            setStamina(value - dt / timeDeplete);
            if (value == 0F)
            {
                noStamina?.Invoke();
            }
        }
        else if (isRest)
        {
            if (value < 1.0)
            {
                setStamina(value + dt / timeRefill);
            }
        }
    }
}
