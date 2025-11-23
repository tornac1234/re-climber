using UnityEngine;


public class respawn : MonoBehaviour
{
    public Stamina stamina;
    public bool IsClimbing;
    public bool IsGrounded;
    public float fallThreshold = -10f;
    public float falldistance;
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
