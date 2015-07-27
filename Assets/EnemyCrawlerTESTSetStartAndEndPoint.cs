using UnityEngine;
using System.Collections;

public class EnemyCrawlerTESTSetStartAndEndPoint : MonoBehaviour
{
    public float duration;
    private float timeSpent = 0;

    // Init
    void Awake()
    {
        if (enabled)
        {
            // Set startpoint
            GetComponent<EnemyCrawlerAI>().startpoint.position = transform.position;
        }
    }

	// Update
	void Update ()
    {
	    if (timeSpent >= duration)
        {
            // Set endpoint
            GetComponent<EnemyCrawlerAI>().endpoint.position = transform.position;

            enabled = false;
        }

        // Increment time
        timeSpent += Time.deltaTime;
	}
}
