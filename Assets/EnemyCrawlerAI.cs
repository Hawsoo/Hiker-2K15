using UnityEngine;
using System.Collections;

public class EnemyCrawlerAI : MonoBehaviour
{
    public Transform startpoint;
    public Transform endpoint;

    public bool allowLogging;

    private EnemyMovement input;

    private const float DEAD_ZONE = 0.001f;

    // Init
    void Awake()
    {
        input = GetComponent<EnemyMovement>();
    }

    // Checks if ended at endpoint
    void FixedUpdate()
    {
        /*if (endpoint.position.x == transform.position.x &&
            endpoint.position.y == transform.position.y)*/
        if (Mathf.Abs(endpoint.position.x - transform.position.x) < DEAD_ZONE &&
            Mathf.Abs(endpoint.position.y - transform.position.y) < DEAD_ZONE)
        {
            if (allowLogging) Debug.Log("Reset");

            // Reset enemy
            transform.position = startpoint.position;
            input.ResetVelocity();

            // TEST: TEMP
            EnemyCrawlerTESTCreateEvenCoords ec = GetComponent<EnemyCrawlerTESTCreateEvenCoords>();
            if (ec != null)
            {
                ec.ResetFlag();
            }
        }
    }

    // Player got hit
    void OnEnemyTouched(Collider other)
    {
        other.SendMessage("GotHit", /*GetComponent<EnemyMovement>().damage,*/ new float[] { -1, transform.position.x });
    }
}
