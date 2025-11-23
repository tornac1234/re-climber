using UnityEngine;


public class respawn : MonoBehaviour
{
    public Stamina stamina;
    public bool IsClimbing;
    public bool IsGrounded;
    public float fallThreshold = -10f;
    public float falldistance;
    public Rigidbody2D rb;
    void Start()
    {
        
    }
    void Update()
    {
        if (falldistance < fallThreshold)
        {
            Debug.Log("Respawn");
            transform.position = new Vector3(0, 0, 0);
            falldistance = 0;
        }
        if (IsGrounded)
        {
            falldistance = 0;
        }
    }

    void FixedUpdate()
    {
        if (!IsClimbing && !IsGrounded && stamina.value>0)
        {
            falldistance += Time.deltaTime*rb.linearVelocity.y; 
        
        }
                
    }
}
