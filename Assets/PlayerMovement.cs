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

    public float knockbackStrength;
    private bool tempDisableMaxSpeed = false;

    public GameObject hurtColliders;
    public bool hurtInvincible = false;
    public float hurtTime;
    private float hurtTimeWaited = 0;
    
    private Vector3 velocity = new Vector3();
    private bool disableLeft = false;
    private bool disableRight = false;
    private bool isRight = true;

    // Shift colliders out of way when invincible
    void Update()
    {
        if (hurtInvincible)
        {
            // Shift colliders out of way
            Vector3 originalPosition = hurtColliders.transform.position;
            hurtColliders.transform.position = new Vector3(originalPosition.x, originalPosition.y, 4.5f);
                        
            originalPosition = GetComponent<CharacterController>().center;
            GetComponent<CharacterController>().center = new Vector3(originalPosition.x, originalPosition.y, 4.5f);

            originalPosition = GetComponent<BoxCollider>().center;
            GetComponent<BoxCollider>().center = new Vector3(originalPosition.x, originalPosition.y, 4.5f);

            // Count down timer
            hurtTimeWaited -= Time.deltaTime;
            if (hurtTimeWaited <= 0)
            {
                // Player is no longer invincible
                hurtInvincible = false;
            }
        }
        else
        {
            // Shift back
            Vector3 originalPosition = hurtColliders.transform.position;
            hurtColliders.transform.position = new Vector3(originalPosition.x, originalPosition.y, 0);

            originalPosition = GetComponent<CharacterController>().center;
            GetComponent<CharacterController>().center = new Vector3(originalPosition.x, originalPosition.y, 0);

            originalPosition = GetComponent<BoxCollider>().center;
            GetComponent<BoxCollider>().center = new Vector3(originalPosition.x, originalPosition.y, 0);
        }
    }

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

        if (!tempDisableMaxSpeed)
        {
            // Limit movement
            if (velocity.x < -maxMoveSpeed) velocity.x = -maxMoveSpeed;
            if (velocity.x > maxMoveSpeed) velocity.x = maxMoveSpeed;
        }
        else
        {
            // Reset when within bounds
            if (velocity.x >= -maxMoveSpeed
                && velocity.x <= maxMoveSpeed)
                tempDisableMaxSpeed = false;
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

    // Got hit
    void GotHit(/*float damage, */float xPos)
    {
        tempDisableMaxSpeed = true;

        if (xPos < transform.position.x)
        {
            // Jump right
            velocity.x = knockbackStrength;
            velocity.y = knockbackStrength;
        }
        else
        {
            // Jump left
            velocity.x = -knockbackStrength;
            velocity.y = knockbackStrength;
        }

        // Apply velocity
        GetComponent<CharacterController>().Move(velocity * Time.deltaTime);

        // Set player invincible for small amount of time
        hurtInvincible = true;
        hurtTimeWaited = hurtTime;
    }
}
