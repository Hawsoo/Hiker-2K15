using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float gravity;
    public float moveSpeed;
    public float friction;

    public float maxMoveSpeed;

    // BETA Inputs //
    public bool moveLeft;
    public bool moveRight;
    //-BETA Inputs-//

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
            velocity.x -= moveSpeed;
        }
        else if (moveRight)
        {
            velocity.x += moveSpeed;
        }
        // If no input
        else
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
        if (velocity.x < -maxMoveSpeed) velocity.x = -maxMoveSpeed;
        if (velocity.x > maxMoveSpeed) velocity.x = maxMoveSpeed;

        // Apply
        c.Move(velocity * Time.deltaTime);
	}
}
