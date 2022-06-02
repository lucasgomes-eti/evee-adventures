using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] int health;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health < 1)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
