using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class respawn : MonoBehaviour
{
    public Stamina stamina;
    public Climbing climbing;
    public Movement movement;
    public float fallThreshold = -3;
    public float fallDistance;
    public float deathCooldown = 3f;
    public bool isDead;
    public Rigidbody2D rb;
    public Transform RespawnReference;

    public static event UnityAction OnDeath;
    public static event UnityAction OnRespawn;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stamina = GetComponent<Stamina>();
    }

    private void Update()
    {
        if (fallDistance < fallThreshold)
        {
            if (movement.IsGrounded && !isDead)
            {
                Debug.Log("Respawn");
                StartCoroutine(Respawn());
                fallDistance = 0;
            }
        }
        if (movement.IsGrounded)
        {
            fallDistance = 0;
        }
    }

    private void FixedUpdate()
    {
        if (!isDead && !climbing.IsClimbing && !movement.IsGrounded)
        {
            fallDistance += Time.deltaTime*rb.linearVelocity.y; 
        }
    }

    public IEnumerator Respawn()   //Activer les aniimations correctes
    {
        isDead = true;
        climbing.enabled = false;
        movement.enabled = false;
        OnDeath?.Invoke();

        yield return new WaitForSeconds(deathCooldown);
        transform.position = RespawnReference.position;
        fallDistance = 0;

        isDead = false;
        climbing.enabled = true;
        movement.enabled = true;

        OnRespawn?.Invoke();
    }
}
