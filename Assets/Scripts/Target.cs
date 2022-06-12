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

    public void Die()
    {
        FindObjectOfType<AudioManager>().Play("EnemyHit");
        Destroy(gameObject);
    }
}
