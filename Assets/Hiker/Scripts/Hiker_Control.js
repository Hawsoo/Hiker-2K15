var speed : float = 6.0;
	var jumpSpeed : float = 4.0;
	var gravity : float = 20.0;
	private var moveDirection : Vector3 = Vector3.zero;
	function Update() {
		
			
		var controller : CharacterController = GetComponent(CharacterController);
			// We are grounded, so recalculate
			// move direction directly from axes
			moveDirection = Vector3(0, 0,
			                        Input.GetAxis("Horizontal"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			
		if (controller.isGrounded) {
			if (Input.GetButton ("Jump")) {
				moveDirection.y = jumpSpeed;
			}
		}
		
		
		if(Input.GetKey ("v"))
		{
		moveDirection = Vector3(0, 0,
			                        Input.GetAxis("Horizontal"));
			moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed * 2;
		}
		//^making running faster than walking, but if you push "v" while crouching you still go fast so yeah...
		
		// Apply gravity
		moveDirection.y -= gravity * Time.deltaTime;
		
		// Move the controller
		controller.Move(moveDirection * Time.deltaTime);
	
}