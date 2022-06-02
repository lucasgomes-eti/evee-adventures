using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 6f;
    Vector3 velocity;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] LayerMask whatIsGround, whatIsObstacle;

    void Update()
    {
        HandleHozirontalMovement();
        HandleJump();
        ApplyGravity();
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
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

    void HandleHozirontalMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 direction = new Vector3(horizontal, 0, 0).normalized;

        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction * speed * Time.deltaTime);
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -transform.up, 1.5f, whatIsGround) || Physics.Raycast(transform.position, -transform.up, 1.5f, whatIsObstacle);
    }
}
