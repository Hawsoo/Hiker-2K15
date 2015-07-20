using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    //public CameraAnimHandler cameraAnimHandler;
    public new GameObject camera;

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

    // Movement Inputs //
    public bool moveLeft;
    public bool moveRight;
    public bool jump;
    /////////////////////
    
    [SerializeField]
    private Vector3 velocity = new Vector3();
    private bool disableLeft = false;
    private bool disableRight = false;
    //private bool isRight = true;

    // TEST -- Testing analytics
    private int anal_deaths = 0;
    private int anal_jumps = 0;
    private int anal_bear1hits = 0;
    private int anal_bear2hits = 0;
    private int anal_bear3hits = 0;
    private int anal_bear4hits = 0;

    // TEST -- Respawn coordinates
    void Awake()
    {
        if (camera != null)
            camera.transform.position = transform.position;
    }

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
            if (jump)
            {
                velocity.y = jumpSpeed;
                anal_jumps++;
            }
        }

        // Add input movement
        if (moveLeft)
            velocity.x -= moveSpeed;
        if (moveRight)
            velocity.x += moveSpeed;

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
        if (!moveLeft && !moveRight)
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

    // Resets the velocity
    public void ResetVelocity()
    {
        velocity = Vector3.zero;
    }

    // Messages
    void HitSide() { velocity.x = 0; }
    void HitTop() { velocity.y = 0; }
    void DisableLeft() { disableLeft = true; }
    void DisableRight() { disableRight = true; }

    // Got hit          // ind0->bearindex, ind1->xPos
    void GotHit(/*float damage, */float[] xPos)
    {
        tempDisableMaxSpeed = true;

        if (xPos[1] < transform.position.x)
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

        // TEST -- apply analytics
        if (xPos[0] == 1)
        {
            anal_bear1hits++;
        }
        else if (xPos[0] == 2)
        {
            anal_bear2hits++;
        }
        else if (xPos[0] == 3)
        {
            anal_bear3hits++;
        }
        else if (xPos[0] == 4)
        {
            anal_bear4hits++;
        }
    }

    // If out of bounds
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Death Trigger")
        {
            // TEST -- For analytics
            anal_deaths++;

            // Respawn
            EntryPointControl.ChangeScenes(Application.loadedLevelName);
        }
    }

    // TEST -- spits out analytics
    void SpitOutAnalytics()
    {
        // Write to file
        System.IO.File.WriteAllText(Application.dataPath + "\\..\\anal.txt", "d=" + anal_deaths + ";\nj=" + anal_jumps + ";\nb1=" + anal_bear1hits + ";\nb2=" + anal_bear2hits + ";\nb3=" + anal_bear3hits + ";\nb4=" + anal_bear4hits);
        
        // Quit app
        Application.Quit();
    }
}
