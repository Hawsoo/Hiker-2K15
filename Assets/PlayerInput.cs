using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    public string horizontalAxis = "Horizontal";
    public string jumpButton = "Jump";
    public float deadzone = 0.75f;

    private PlayerMovement input;

    // Init
    void Awake()
    {
        input = GetComponent<PlayerMovement>();
    }

	// Update
	void FixedUpdate()
    {
        input.moveLeft = Input.GetAxisRaw(horizontalAxis) < -deadzone;
        input.moveRight = Input.GetAxisRaw(horizontalAxis) > deadzone;
        input.jump = Input.GetButton(jumpButton);
	}
}
