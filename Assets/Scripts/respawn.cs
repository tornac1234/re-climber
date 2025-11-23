using UnityEngine;

public Stamina stamina;
public bool IsClimbing;
public bool IsGrounded;
public float fallThreshold = -10f;
public float falldistance;
public class respawn : MonoBehaviour
{
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (!IsClimbing && !IsGrounded && stamina.value>0)
        {
            falldistance += Time.deltaTime*rb.linearVelocity.y; 
        }
                
    }
}
