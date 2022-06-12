using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] float speed = 6f;
    [SerializeField] LayerMask whatIsObstacle, whatIsPlayer;
    Transform player;
    Vector3 velocity;

    bool directionFlag
    {
        get { return direction == -1 ? false : true; }
        set { direction = value ? 1 : -1; }
    }
    int direction = -1;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleHozirontalMovement();
        ApplyGravity();
    }

    void HandleHozirontalMovement()
    {
        Vector3 direction = new Vector3(this.direction, 0, 0).normalized;

        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction * speed * Time.deltaTime);
        }
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name != transform.gameObject.name && (whatIsObstacle == (whatIsObstacle | (1 << collision.gameObject.layer))))
        {
            Debug.Log($"Collision detected with {collision.gameObject.name}");
            directionFlag = !directionFlag;
            if(whatIsPlayer == (whatIsPlayer | (1 << collision.gameObject.layer)))
            {
                player.GetComponent<PlayerController>().TakeDamage();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeathZone")
        {
            GetComponent<Target>().Die();
        }
    }
}
