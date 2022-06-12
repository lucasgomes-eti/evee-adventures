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
    [SerializeField] GameObject inGameMenuUI;
    [SerializeField] Text levelCompleteText;

    private void Start()
    {
        livesText.text = $"Lives: {lives}";
        scoreText.text = $"Score: {score}";
        levelCompleteText.enabled = false;
        inGameMenuUI.SetActive(false);
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
            return;
        }
        FindObjectOfType<AudioManager>().Play("PlayerHit");
    }

    void Die()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        Destroy(gameObject);
        inGameMenuUI.SetActive(true);
        levelCompleteText.enabled = true;
        levelCompleteText.text = "YOU DIED";
        levelCompleteText.color = Color.red;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Coin")
        {
            score += 5;
            scoreText.text = $"Score: {score}";
            FindObjectOfType<AudioManager>().Play("CoinCollected");
            Destroy(other.gameObject);
        }

        if(other.tag == "DeathZone")
        {
            Die();
        }

        if(other.tag == "EndCube")
        {
            levelCompleteText.enabled = true;
            inGameMenuUI.SetActive(true);
            transform.gameObject.SetActive(false);
        }
    }
}
