using System.Collections;
using UnityEngine;


public class respawn : MonoBehaviour
{
    public Stamina stamina;
    public Climbing climbing;
    public Movement movement;
    public float fallThreshold = -10f;
    public float falldistance;
    public float deathCooldown = 3f;
    public Rigidbody2D rb;
    void Start()    
    {
        rb = GetComponent<Rigidbody2D>();
        stamina = GetComponent<Stamina>();
    }
    void Update()
    {
        if (falldistance < fallThreshold)
        {
            Debug.Log("Respawn");
            if (movement.IsGrounded)
            {
                StartCoroutine(Respawn());

            }
            falldistance = 0;
        }
        if (movement.IsGrounded)
        {
            falldistance = 0;
        }
    }

    void FixedUpdate()
    {
        if (!climbing.IsClimbing && !movement.IsGrounded && stamina.value>0)
        {
            falldistance += Time.deltaTime*rb.linearVelocity.y; 
        }
    }
    IEnumerator Respawn()   //Activer les aniimations correctes
    {   
        climbing.enabled = false;
        movement.enabled = false;
        yield return new WaitForSeconds(deathCooldown);
        transform.position = new Vector3(0f, -2.5f, 0f);
    
        climbing.enabled = true;
        movement.enabled = true;
    }
}
