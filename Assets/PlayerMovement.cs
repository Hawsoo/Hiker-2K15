using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public CameraAnimHandler cameraAnimHandler;

    public float gravity;
    public float jumpSpeed;
    public float moveStrength;
    public float friction;

    public bool onGround = false;

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
            // Jump
            if (Input.GetButton("Jump"))
            {
                velocity.y = jumpSpeed;
            }
        }

        // Add input movement
        velocity.x += Input.GetAxisRaw("Horizontal") * moveStrength;

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
        //GetComponent<Rigidbody>().velocity = velocity;
        GetComponent<CharacterController>().Move(velocity * Time.deltaTime);

        // Reset variables
        onGround = false;
        disableLeft = false;
        disableRight = false;
	}

    // Check if on ground
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }

    // Messages
    void HitSide() { velocity.x = 0; }
    void HitTop() { velocity.y = 0; }
    void DisableLeft() { disableLeft = true; }
    void DisableRight() { disableRight = true; }
}
