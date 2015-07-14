using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float gravity;
    public float moveSpeed;
    public float friction;

    public float maxMoveSpeed;

    // Charging series
    public bool isCharging = false;

    public float chargeSpeed;
    public float maxChargeSpeed;

    // TEST
    public float damage = 1;

    // Movement Inputs //
    public bool moveLeft;
    public bool moveRight;
    /////////////////////

    private Vector3 velocity = new Vector3();
	
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
        }

        // Input Movement
        if (moveLeft)
        {
            if (isCharging)
                velocity.x -= chargeSpeed;
            else
                velocity.x -= moveSpeed;
        }
        else if (moveRight)
        {
            if (isCharging)
                velocity.x += chargeSpeed;
            else
                velocity.x += moveSpeed;
        }
        // If no input (and grounded)
        else if (c.isGrounded)
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

        // Limit movement
        if (isCharging)
        {
            if (velocity.x < -maxChargeSpeed) velocity.x = -maxChargeSpeed;
            if (velocity.x > maxChargeSpeed) velocity.x = maxChargeSpeed;
        }
        else
        {
            if (velocity.x < -maxMoveSpeed) velocity.x = -maxMoveSpeed;
            if (velocity.x > maxMoveSpeed) velocity.x = maxMoveSpeed;
        }

        // Apply
        c.Move(velocity * Time.deltaTime);
	}
}
