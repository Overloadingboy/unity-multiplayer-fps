using Mirror;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SyncVar] private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    [Server]
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage. Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    [Server]
    private void Die()
    {
        Debug.Log(gameObject.name + " died!");
        NetworkServer.Destroy(gameObject);
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
