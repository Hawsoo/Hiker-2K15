using UnityEngine;
using System.Collections;

public class EnemyBearChaserAI : MonoBehaviour
{
    public bool startCharged;

    [SerializeField]
    private GameObject chargeObj;
    private EnemyMovement input;

    // TEST -- for analytics
    public float index;

	// Init
	void Awake()
    {
        input = GetComponent<EnemyMovement>();

        if (startCharged)
        {
            input.moveLeft = input.moveRight = false;
            input.isCharging = true;
        }
	}

    // Update
    void Update()
    {
        if (chargeObj != null && input.isCharging)
        {
            // Charge forever
            input.moveLeft = input.moveRight = false;

            if (chargeObj.transform.position.x < transform.position.x)
            {
                // Move left
                input.moveLeft = true;
            }
            else
            {
                // Move right
                input.moveRight = true;
            }
        }
    }

    // Start charging
    void OnAttackTrigger(Collider other)
    {
        chargeObj = other.gameObject;

        // Player triggered event;
        // keep charging on player
        input.moveLeft = input.moveRight = false;
        input.isCharging = true;
    }

    // Player got hit
    void OnEnemyTouched(Collider other)
    {
        other.SendMessage("GotHit", /*GetComponent<EnemyMovement>().damage,*/ new float[] { index, transform.position.x });
    }

    #region Wall Collisions

    // Touched wall at left: go right
    void OnWallTouchedL(Collider other)
    {
        // Reset horizontal velocity
        GetComponent<EnemyMovement>().SetHspeed(/*-1*/0);

        /*if (input.isCharging)
        {
            // Reset; hold there
            input.moveLeft = input.moveRight = input.isCharging = false;
            moveTimeWaited = 0;
        }
        else*/
        {
            // Change directions
            /*input.isCharging = false;
            moveTimeWaited = 0;*/

            // Move right
            input.moveLeft = false;
            input.moveRight = true;
        }
    }

    // Touched wall at right: go left
    void OnWallTouchedR(Collider other)
    {
        // Reset horizontal velocity
        GetComponent<EnemyMovement>().SetHspeed(/*1*/0);

        /*if (input.isCharging)
        {
            // Reset; hold there
            input.moveLeft = input.moveRight = input.isCharging = false;
            moveTimeWaited = 0;
        }
        else*/
        {
            // Change directions
            /*input.isCharging = false;
            moveTimeWaited = 0;*/

            // Move left
            input.moveLeft = true;
            input.moveRight = false;
        }
    }

    #endregion
}
