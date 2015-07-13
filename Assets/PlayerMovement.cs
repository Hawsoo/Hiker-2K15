using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public CameraAnimHandler cameraAnimHandler;

    public float gravity;
    public float jumpSpeed;
    public float moveSpeed;
    public float friction;

    public float maxMoveSpeed;

    private Vector3 velocity = new Vector3();
    private bool disableLeft = false;
    private bool disableRight = false;
    private bool isRight = true;

    // Update
	void FixedUpdate()
    {
        CharacterController c = GetComponent<CharacterController>();

        if (!c.isGrounded)
        {
            // Push gravity
            velocity.y -= gravity;
        }
        else
        {
            // Reset gravity
            velocity.y = -gravity;

            // Jump
            if (Input.GetButton("Jump"))
            {
                velocity.y = jumpSpeed;
            }
        }

        // Add input movement
        velocity.x += Input.GetAxisRaw("Horizontal") * moveSpeed;

        // Limit movement
        if (velocity.x < -maxMoveSpeed) velocity.x = -maxMoveSpeed;
        if (velocity.x > maxMoveSpeed) velocity.x = maxMoveSpeed;

        if (c.isGrounded)
        {
            // Change direction
            if (isRight && velocity.x < 0)
            {
                // Turn left
                isRight = false;
                cameraAnimHandler.SetCamDirection(isRight);
            }
            else if (!isRight && velocity.x > 0)
            {
                // Turn left
                isRight = true;
                cameraAnimHandler.SetCamDirection(isRight);
            }
        }

        // Disable if needed
        if (disableLeft) { velocity.x = Mathf.Max(0, velocity.x); }
        if (disableRight) { velocity.x = Mathf.Min(0, velocity.x); }

        // If no input
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            // Apply Friction
            if (velocity.x < 0)
            {
                velocity.x += friction;
                if (velocity.x > 0) velocity.x = 0;
            }
            else if (velocity.x > 0)
            {
                velocity.x -= friction;
                if (velocity.x < 0) velocity.x = 0;
            }
        }

        // Apply velocity
        c.Move(velocity * Time.deltaTime);

        // Reset variables
        disableLeft = false;
        disableRight = false;
	}

    // Messages
    void HitSide() { velocity.x = 0; }
    void HitTop() { velocity.y = 0; }
    void DisableLeft() { disableLeft = true; }
    void DisableRight() { disableRight = true; }
}
