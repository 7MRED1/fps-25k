
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f && !gameObject.name.StartsWith("Player"))
        {
            Destroy(gameObject);
        }
        else if (currentHealth <= 0f)
        {
            // Player death logic could go here
            Debug.Log("Player died!");
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
    }
}
