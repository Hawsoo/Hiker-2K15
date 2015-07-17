using UnityEngine;
using System.Collections;

public class EnemyBearChaserAI : MonoBehaviour
{
    /*
     * Actions for bears:
     * -Walking/stopping
     * -Charging
     */
    /*public float moveTime;
    private float moveTimeWaited = -1;*/

    private EnemyMovement input;
    private GameObject chargeObj;

	// Init
	void Awake()
    {
        input = GetComponent<EnemyMovement>();
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
	
	// Update
	/*void Update()
    {
        if (moveTimeWaited >= moveTime
            || moveTimeWaited == -1 /* First time case *//*)
        {
            // Change actions
            ChooseNewAction();
        }
        else
        {
            // Count up
            moveTimeWaited += Time.deltaTime;
        }
	}

    // Changes the action
    private void ChooseNewAction()
    {
        // Reset
        input.moveLeft = input.moveRight = input.isCharging = false;
        moveTimeWaited = 0;

        // Choose (3 cases)
        int randcase = Random.Range(1, 4);

        switch (randcase)
        {
            case 1:
                // Move left
                input.moveLeft = true;
                break;
            
            case 2:
                // Move right
                input.moveRight = true;
                break;

            case 3:
                // Don't move at all
                break;

            default:
                Debug.Log("ERROR IN CODE");
                break;
        }
    }*/

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
        other.SendMessage("GotHit", /*GetComponent<EnemyMovement>().damage,*/ transform.position.x);
    }

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
}
