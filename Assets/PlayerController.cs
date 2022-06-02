using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask whatIsEnemy;
    [SerializeField] float attackDistance;
    [SerializeField] int damage;
    int lives = 3;
    int score = 0;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    private void Start()
    {
        livesText.text = $"Lives: {lives}";
        scoreText.text = $"Score: {score}";
    }

    void FixedUpdate()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(transform.position, -transform.up, out hitInfo, attackDistance, whatIsEnemy))
        {
            Target target = hitInfo.transform.GetComponent<Target>();
            score += 10;
            scoreText.text = $"Score: {score}";
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    public void TakeDamage()
    {
        lives--;
        livesText.text = $"Lives: {lives}";
        if(lives < 1)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Coin")
        {
            score += 5;
            scoreText.text = $"Score: {score}";
            Destroy(other.gameObject);
        }
    }
}
