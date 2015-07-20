using UnityEngine;
using System.Collections;

public class EnemyBatAI : MonoBehaviour
{
    public bool startRight = true;
    private EnemyMovement input;

	// Init
	void Awake()
    {
        input = GetComponent<EnemyMovement>();

        if (startRight)
        {
            // Move right
            input.moveLeft = false;
            input.moveRight = true;
        }
        else
        {
            // Move left
            input.moveLeft = true;
            input.moveRight = false;
        }
	}

    // Player got hit
    void OnEnemyTouched(Collider other)
    {
        other.SendMessage("GotHit", /*GetComponent<EnemyMovement>().damage,*/ new float[] {-1, transform.position.x});
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
            // Move left
            input.moveLeft = true;
            input.moveRight = false;
        }
    }
}
