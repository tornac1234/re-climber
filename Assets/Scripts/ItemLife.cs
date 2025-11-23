using UnityEngine;

public class ItemLife : MonoBehaviour
{
    public int lives;

    public void Start()
    {
        respawn.OnDeath += TakeDamage;
    }

    private void OnDestroy()
    {
        respawn.OnDeath -= TakeDamage;
    }

    public void TakeDamage()
    {
        lives--;
        if (lives == 0)
        {
            Destroy(gameObject);
        }
    }
}
